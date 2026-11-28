# Heis – animert inspirasjonsdemo for økt 4

Dette er **ferdig kode til inspirasjon**, ikke noe som skal bygges fra bunnen av i undervisningen. Heisen består av fem etasjer. Det er **fire animasjonssteg per etasje**, med synlige mellomposisjoner, og alle etasje- og kupéknapper kan lyse til heisen kommer fram.

## Kjør

Åpne `HeisDemo.slnx` i Visual Studio eller kjør `dotnet run --project HeisDemo.csproj` fra denne mappen. Programmet forsøker å **sette konsollvinduet til 80 × 50** ved oppstart. Dette fungerer i Windows-konsoller som tillater størrelsesendring. Overskriften «HEIS | Instruksjon» står på én linje, slik at både de gule kupéknappene og statuslinjen med posisjon, dør og mål får plass. Animasjonen tegnes uten linjeskift, så terminalen skal ikke rulle når heisen beveger seg. Dersom terminalen ignorerer størrelsesendringen, kan programmet likevel kjøre så lenge den synlige flaten har minst 77 kolonner og 30 linjer; ellers vises en beskjed om å bruke et større eller eksternt konsollvindu. Animasjonen tegnes i konsollen, ikke som et GUI. Avslutt med Esc.

## Kontroller

- Trykk **1–5** direkte for å bestille en etasje **fra innsiden av heisen**. Knappen lyser **gult** (`●`) til heisen stopper. Avslåtte knapper vises mørkegrå (`○`).
- Trykk **H**, deretter etasjenummer **1–5**, og velg **piltast opp (↑)** eller **piltast ned (↓)** for å simulere en uteknapp. Retningsinstruksjonen vises først når du har valgt H og etasje. Uteknappen lyser gult til heisen kommer.
- Ved endetasjene er én retning ugyldig: ikke opp fra 5 eller ned fra 1.
- Hvis heisen allerede er på vei mot en etasje, **stopper den også ved bestilte etasjer underveis**: kupébestillinger og uteknapper som peker i samme retning som heisen kjører. For eksempel går turen fra 5 til 1 via bestilte stopp i 3 og 2. En uteknapp for ned i 2 betjenes på vei ned, men en uteknapp for opp i 2 blir stående til heisen kan betjene den i riktig retning.
- Når hovedmålet er nådd, betjener demoen alle aktive knapper på den etasjen. Når det ikke finnes et aktivt hovedmål, velger heisen den nærmeste gjenværende bestillingen. Dette er forenklet ruting, ikke full heisalgoritme.
- Animasjonen går **210 ms per steg**, 50 % saktere enn tidligere 140 ms.

## Hva demonstrerer innkapsling?

Heisen er delt opp i små objekter med tydelige ansvar:

- `Elevator` **koordinerer** de andre objektene. Den velger en aktiv bestilling, flytter heisen, fullfører bestillinger og åpner døren.
- `ElevatorRequest` representerer én fysisk knapp: `Floor` og `Type` er uforanderlige, mens `IsActive` bare kan endres med `Activate()` og `Complete()`.
- `ElevatorRequests` **eier et privat array** med alle lovlige bestillinger. Med fem etasjer er det 5 kupéknapper, 4 oppknapper og 4 nedknapper – **13 objekter totalt**. Ingen nye bestillingsobjekter blir opprettet ved tastetrykk: vi aktiverer en av de eksisterende. Ugyldige knapper, som «opp» fra øverste etasje, finnes ikke i arrayet.
- `RequestType` er en `enum` med `Cabin`, `FloorUp` og `FloorDown`.
- `ElevatorMovement` eier posisjonen og flytter ett av fire animasjonssteg per etasje. `Elevator` får kun be den om å flytte ett steg mot en gyldig etasje.
- `ElevatorDoor` eier nedtellingen som holder døren åpen. Den kan ikke få negativ nedtelling ved vanlig bruk.
- `ElevatorView` tegner heisen ved å lese dens offentlige egenskaper. `Program.cs` leser tastetrykk og sender bestillinger.

Det finnes **ingen `List<T>`** i modellen. Det faste arrayet passer godt fordi alle mulige knapper er kjent på forhånd.

Se særlig hvordan `Elevator` bruker en `ElevatorRequest?` som aktiv bestilling. Når den er `null`, har heisen ingen aktiv destinasjon. Dette er tydeligere enn å bruke et magisk tall som `0`.

**Viktig innkapslingsregel:** Mens døren er åpen, lar `Elevator.Tick()` bare døren telle ned. Heisen kan ikke flyttes direkte fra `Program.cs`, og request-arrayet blir aldri eksponert for klienten.

**Begrensning:** Dette er en pedagogisk simulator, ikke en realistisk heisstyring. Ved mellomstopp betjenes bare kupébestillinger og uteknapper i kjøreretningen. Ved hovedmålet betjenes alle forespørsler der, også i motsatt retning, for å unngå komplisert ruting. Det finnes ikke sikkerhetssensorer, vektdetektering eller optimal ruting.

**Status:** Koden er statisk gjennomgått. Full kompilering og interaktiv prøvekjøring i Visual Studio gjenstår.
