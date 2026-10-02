Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Glosprogram");

List<Word> words = [];
List<(string From, string To)> pairs = [];

// läser in alla ordlistor, filnamnet anger språken (swedish-english.csv)
foreach (string file in Directory.GetFiles("wordlists", "*.csv").Order())
{
  string[] languages = Path.GetFileNameWithoutExtension(file).Split("-");
  pairs.Add((languages[0], languages[1]));

  foreach (string line in File.ReadAllLines(file))
  {
    string[] pair = line.Split(",");
    // lägg till ordet i båda riktningarna, så behöver vi aldrig vända på något senare
    words.Add(new Word(languages[0], languages[1], pair[0].Trim(), pair[1].Trim()));
    words.Add(new Word(languages[1], languages[0], pair[1].Trim(), pair[0].Trim()));
  }
}

while (true)
{
  int? pairChoice = Choose("Välj språkpar:", pairs.Select(p => $"{p.From} – {p.To}").ToList());
  if (pairChoice == null) break;

  var (first, second) = pairs[pairChoice.Value];
  List<(string From, string To)> directions = [(first, second), (second, first)];

  int? directionChoice = Choose("Riktning:", directions.Select(d => $"{d.From} → {d.To}").ToList());
  if (directionChoice == null) break;

  var (from, to) = directions[directionChoice.Value];
  LookupLoop(from, to);
}

// visar en numrerad meny och frågar tills svaret är giltigt, null betyder avsluta
int? Choose(string prompt, List<string> options)
{
  while (true)
  {
    Console.WriteLine();
    Console.WriteLine(prompt);
    for (int i = 0; i < options.Count; i++)
    {
      Console.WriteLine($"  {i + 1}) {options[i]}");
    }
    Console.WriteLine("  q) Avsluta");
    Console.Write("> ");

    string? input = Console.ReadLine()?.Trim();
    if (input == null || input == "q") return null;
    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Count) return choice - 1;

    Console.WriteLine("Ogiltigt val, försök igen.");
  }
}

// slår upp ord tills användaren skriver en tom rad
void LookupLoop(string from, string to)
{
  // key: ordet jag vill översätta från, value: alla översättningar
  Dictionary<string, List<string>> dictionary = words
    .Where(word => word.FromLanguage == from && word.ToLanguage == to)
    .GroupBy(word => word.FromWord, StringComparer.OrdinalIgnoreCase)
    .ToDictionary(
      group => group.Key,
      group => group.Select(word => word.ToWord).ToList(),
      StringComparer.OrdinalIgnoreCase
    );

  Console.WriteLine();
  Console.WriteLine("Skriv ett ord (tom rad = byt språk, ord = översättning för att lägga till):");
  while (true)
  {
    Console.Write("> ");
    string input = Console.ReadLine()?.Trim() ?? "";
    if (input == "") return;

    if (input.Contains('='))
    {
      AddWord(from, to, input, dictionary);
    }
    else if (dictionary.TryGetValue(input, out List<string>? translations))
    {
      Console.WriteLine($"{input.ToLower()} → {string.Join(", ", translations)}");
    }
    else
    {
      Console.WriteLine($"Ordet \"{input}\" finns inte i ordlistan.");
    }
  }
}

// lägger till "ord = översättning" i ordboken och sparar det i rätt ordlistefil
void AddWord(string from, string to, string input, Dictionary<string, List<string>> dictionary)
{
  string[] parts = input.Split("=");
  if (parts.Length != 2 || parts[0].Trim() == "" || parts[1].Trim() == "" || input.Contains(','))
  {
    Console.WriteLine("Skriv på formen: ord = översättning (utan kommatecken).");
    return;
  }

  string fromWord = parts[0].Trim();
  string toWord = parts[1].Trim();

  if (dictionary.TryGetValue(fromWord, out List<string>? existing) && existing.Contains(toWord, StringComparer.OrdinalIgnoreCase))
  {
    Console.WriteLine($"\"{fromWord} → {toWord}\" finns redan.");
    return;
  }

  if (existing == null)
  {
    existing = [];
    dictionary[fromWord] = existing;
  }
  existing.Add(toWord);
  words.Add(new Word(from, to, fromWord, toWord));
  words.Add(new Word(to, from, toWord, fromWord));

  // filen följer filnamnets ordning, så paret vänds om riktningen är omvänd
  bool forward = pairs.Contains((from, to));
  string file = forward ? $"{from}-{to}.csv" : $"{to}-{from}.csv";
  string line = forward ? $"{fromWord},{toWord}" : $"{toWord},{fromWord}";
  File.AppendAllText(Path.Combine("wordlists", file), line + Environment.NewLine);

  Console.WriteLine($"Sparade: {fromWord} → {toWord}");
}
