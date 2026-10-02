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
  new Word("swedish", "english", "glad", "glad")

];

// key  // value
Dictionary<string, List<Word>> swedishToEnglish = words.GroupBy(word => word.FromWord)
.ToDictionary(
  group => group.Key,     // ordet jag vill översätta från
  group => group.ToList() // lista med våra word objekt som matchar nyckeln
);

foreach (var word in swedishToEnglish["snabb"])
{
  Console.WriteLine($"Översättning: {word.ToWord}");
}