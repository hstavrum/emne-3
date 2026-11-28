using System.Text;

Console.OutputEncoding = Encoding.UTF8;
// Prøv å gjøre Windows-konsollen stor nok én gang ved oppstart.
// Et stort vindu gir god luft rundt tegningen og hindrer rulling.
const int desiredWidth = 80;
const int desiredHeight = 50;

if (OperatingSystem.IsWindows() && !Console.IsOutputRedirected)
{
    try
    {
        // Bufferen må først være stor nok til at vinduet kan utvides.
        Console.SetBufferSize(
            Math.Max(Console.BufferWidth, desiredWidth),
            Math.Max(Console.BufferHeight, desiredHeight));
        Console.SetWindowSize(desiredWidth, desiredHeight);

        // Fjern unødvendig rulleområde når terminalen tillater det.
        Console.SetBufferSize(desiredWidth, desiredHeight);
    }
    catch (IOException) { } // Noen terminaler bestemmer vindusstørrelsen selv.
    catch (ArgumentOutOfRangeException) { } // Skjermen kan være for liten.
    catch (System.Security.SecurityException) { }
}

// Hvis automatisk størrelse ikke støttes, er det nok med plass til selve
// tegningen. 50 linjer er ønsket størrelse, ikke et absolutt krav.
var elevator = new Elevator(5);
int minimumHeight = ElevatorView.RequiredHeight(elevator.FloorCount);
if (Console.WindowWidth < 77 || Console.WindowHeight < minimumHeight)
{
    Console.WriteLine($"Heisen trenger minst 77 kolonner og {minimumHeight} linjer.");
    Console.WriteLine("Utvid terminalvinduet, eller bruk et eksternt konsollvindu.");
    return;
}

Console.CursorVisible = false;
Console.Clear();

string status = "";
int mode = 0;       // 0: kupé, 1: vent på etasje utenfra, 2: vent på retning
int hallFloor = 0;

try
{
    while (true)
    {
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) return;

            if (key.Key == ConsoleKey.H)
            {
                mode = 1;
                status = "";
                continue;
            }

            int digit = key.KeyChar >= '1' && key.KeyChar <= '9'
                ? key.KeyChar - '0'
                : 0;

            if (mode == 1)
            {
                if (digit >= 1 && digit <= elevator.FloorCount)
                {
                    hallFloor = digit;
                    mode = 2;
                    status = "";
                }
                
                continue;
            }

            if (mode == 2)
            {
                if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)
                {
                    bool up = key.Key == ConsoleKey.UpArrow;
                    bool accepted = elevator.CallFromHall(hallFloor, up);
                    status = accepted ? "" : "Denne retningen er ikke mulig fra endetasjen.";
                    mode = 0;
                }
                
                continue;
            }

            if (digit >= 1 && digit <= elevator.FloorCount)
            {
                elevator.RequestFromCabin(digit);
                status = "";
            }
        }

        elevator.Tick();
        string instruction = mode == 1
            ? $"Velg etasje (1–{elevator.FloorCount}) for å kalle heisen."
            : mode == 2 ? $"Etasje {hallFloor}: bruk piltast ↑ for opp eller ↓ for ned." : "";
        ElevatorView.Draw(elevator, instruction, status);
        Thread.Sleep(210); // 50 % saktere enn 140 ms per animasjonssteg
    }
}
finally
{
    Console.CursorVisible = true;
    Console.ResetColor();
    Console.Clear();
}
