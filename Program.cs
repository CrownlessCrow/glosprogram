Console.WriteLine("Glosprogram");

// snabb, fast 

List<Word> words = [
  /*["snabb", "fast"],
  ["glad", "glad"],*/
  new Word("swedish", "english", "snabb", "fast"), // minnesreferens skapas
  new Word("swedish", "english", "snabb", "quick"),
  new Word("swedish", "english", "snabb", "rapid"),
  new Word("swedish", "english", "snabb", "speedy"),
  new Word("swedish", "english", "glad", "happy"),
  new Word("swedish", "english", "glad", "glad"),
  new Word("swedish", "english", "stark", "strong"),
  new Word("swedish", "english", "stark", "powerful"),
  new Word("swedish", "italian", "snabb", "rapido"),
  new Word("swedish", "italian", "stark", "forte"),
  new Word("swedish", "italian", "stark", "robusto"),
  new Word("swedish", "italian", "glad", "felice"),

];

while (true)
{

  Console.WriteLine("Välj vilka språk du vill översätta mellan, 1:");
  string? fromLanguage = Console.ReadLine();

  Console.WriteLine("2:");
  string? toLanguage = Console.ReadLine();

  Console.WriteLine($"Vill du översätta från {fromLanguage} till {toLanguage} eller tvärtom?");
  string? response = Console.ReadLine();

  bool reverse = false;

  if (response == "tvärtom")
  {
    reverse = true;
  }

  // felhantera genom att kolla om språket jag ber om finns som toLanguage i listan

  // key  // value
  Dictionary<string, List<Word>> wordDict = words
  .Where(word => word.FromLanguage == fromLanguage)
  .Where(word => word.ToLanguage == toLanguage)
  .GroupBy(word => reverse ? word.ToWord : word.FromWord)
  .ToDictionary(
    group => group.Key,     // ordet jag vill översätta från
    group => group.ToList() // lista med våra word objekt som matchar nyckeln
  );

  Console.WriteLine("Välj vilket ord du vill översätta från");
  string? fromWord = Console.ReadLine();

  foreach (var word in wordDict[fromWord!])
  {
    Console.WriteLine($"Översättning: {(reverse ? word.FromWord : word.ToWord)}");
  }

}