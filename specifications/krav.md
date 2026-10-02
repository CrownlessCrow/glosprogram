# Krav på användarupplevelsen: Glosprogram

## Måste
| # | Krav | Status |
|---|------|--------|
| U1 | Om ordet saknas visas ett meddelande, och programmet kraschar inte. | ✅ |
| U2 | Ogiltiga menyval ger ett felmeddelande och samma fråga igen. | ✅ |
| U3 | Användaren kan avsluta med `q`. | ✅ |
| U4 | Alla ordlistor i `wordlists/` läses in automatiskt. | ✅ |
| U5 | Inga felsökningsutskrifter visas vid start. | ✅ |

## Bör
| # | Krav | Status |
|---|------|--------|
| U6 | Språkpar väljs i en numrerad meny. | ✅ |
| U7 | Riktning väljs i en meny. | ✅ |
| U8 | Stora och små bokstäver spelar ingen roll, och mellanslag runt ordet tas bort. | ✅ |
| U9 | Man kan slå upp flera ord i rad. En tom rad leder tillbaka till språkmenyn. | ✅ |
| U10 | Resultatet visas på en rad: `snabb → fast, quick`. | ✅ |

## Lägga till ord
| # | Krav | Status |
|---|------|--------|
| U11 | `ord = översättning` i uppslagsläget lägger till ett ordpar, både nya ord och nya synonymer. | ✅ |
| U12 | När ett ord saknas visas ett tips: *Lägg till med: hej = översättning* | ❌ |
| U13 | Ordparet sparas direkt i rätt CSV-fil. Paret vänds om riktningen är omvänd, så filen alltid följer filnamnets ordning. | ✅ |
| U14 | Ordparet sparas inte om en del är tom, innehåller kommatecken eller redan finns, och användaren får veta varför. | ✅ |
