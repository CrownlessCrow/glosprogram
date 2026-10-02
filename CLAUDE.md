# Glosprogram

Ett konsolprogram i C# (.NET 10) för att slå upp och lägga till översättningar mellan språkpar. Kraven finns i [specifications/krav.md](specifications/krav.md). Uppdatera statuskolumnen där när ett krav blir klart.

## Kodstil

Koden är skriven för att vara lätt att läsa för någon som lär sig programmera. Välj det enklaste sättet som fungerar.

### Struktur
- All logik ligger i `Program.cs` som top-level statements. Huvudflödet står överst och ska gå att läsa på några rader.
- Lägg hjälpkod i lokala funktioner under huvudflödet, t.ex. `Choose`, `LookupLoop` och `AddWord`. Skapa inga nya klasser, filer, interfaces eller lager om det inte verkligen behövs.
- Datatyper skrivs som `record` på en rad, t.ex. `record Word(string FromLanguage, string ToLanguage, string FromWord, string ToWord);`.
- Gör beteende till data i stället för flaggor. Ett exempel är riktningen, som är ett `(from, to)`-par och inte en `reverse`-bool.

### Formatering
- Indrag med 2 mellanslag.
- Klammerparenteser på egen rad (Allman-stil).
- Använd `var` när typen syns på högersidan, annars skrivs typen ut (`string input`, `int? choice`).
- Använd moderna C#-konstruktioner som är lätta att läsa: collection expressions (`[]`), tuples med namn (`(string From, string To)`), `TryGetValue` och interpolerade strängar.
- LINQ används när det gör koden kortare och tydligare, t.ex. för att bygga en dictionary. Enkla loopar skrivs som `foreach` eller `for`.

### Namn och språk
- Kod, variabler och funktionsnamn skrivs på engelska.
- Kommentarer och all text som användaren ser skrivs på svenska.
- Kommentarer börjar med liten bokstav och står ovanför funktionen eller raden. De förklarar *varför* något görs, inte vad koden gör, t.ex. `// lägg till ordet i båda riktningarna, så behöver vi aldrig vända på något senare`.
- Varje lokal funktion har en kort kommentar på en rad som beskriver vad den gör.

### Robusthet
- Programmet får inte krascha på något som användaren skriver. Använd `TryGetValue`, `int.TryParse` och kontroller av null i stället för undantag.
- Jämför ord med `StringComparer.OrdinalIgnoreCase` och använd `Trim()` på inmatning.
- Felmeddelanden säger vad som gick fel och hur användaren gör rätt, t.ex. `Skriv på formen: ord = översättning (utan kommatecken).`

## Data
- Ordlistorna ligger i `wordlists/<språk1>-<språk2>.csv`, med ett ordpar per rad: `ord1,ord2`.
- Filnamnet anger språken. En ny fil dyker upp i menyn utan att koden ändras.
- Nya ordpar sparas alltid i den ordning som filnamnet anger.
- Tester som skriver till ordlistorna ska ta en kopia av `wordlists/` först och lägga tillbaka den efteråt.
