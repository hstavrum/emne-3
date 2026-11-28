using System.Text;

// Konsollvisningen er skilt fra selve heisen. Her kan vi pynte på utskriften
// uten å gi Program.cs tilgang til heisens private tilstand.
public static class ElevatorView
{
    // Fem overskriftslinjer + sjakten + tre bunnlinjer.
    // Én ekstra linje sørger for at vi ikke tegner nederst i konsollen.
    public static int RequiredHeight(int floorCount) => 5 + (floorCount - 1) * 4 + 5 + 3 + 1;

    public static void Draw(Elevator elevator, string instruction, string status)
    {
        const int width = 76;
        int shaftRows = (elevator.FloorCount - 1) * 4 + 5;
        var lines = new StringBuilder();
        lines.AppendLine("HEIS | Instruksjon:");
        lines.AppendLine("1–5: velg etasje inne i heisen   H: kall fra etasje   Esc: avslutt");
        lines.AppendLine(instruction);
        lines.AppendLine(status);
        lines.AppendLine(new string('─', 65));

        int carCenterRow = shaftRows - 3 - elevator.PositionQuarter;
        for (int row = 0; row < shaftRows; row++)
        {
            string cabin = row == carCenterRow - 1 ? "┌──────┐"
                         : row == carCenterRow ? (elevator.DoorOpen ? "│ ÅPEN │" : "│ [██] │")
                         : row == carCenterRow + 1 ? "└──────┘"
                         : "        ";

            string floorLabel = "";
            for (int floor = 1; floor <= elevator.FloorCount; floor++)
            {
                if (row == shaftRows - 3 - (floor - 1) * 4)
                {
                    char up = elevator.UpButtonLit(floor) ? '●' : '○';
                    char down = elevator.DownButtonLit(floor) ? '●' : '○';
                    floorLabel = $" {floor}. etg   ↑ {up}   ↓ {down}";
                }
            }

            lines.AppendLine($" ║{cabin}║ ─{floorLabel}");
        }

        lines.AppendLine(new string('─', 65));
        var panel = new StringBuilder("Inne i heisen: ");
        for (int floor = 1; floor <= elevator.FloorCount; floor++)
        {
            char light = elevator.CabButtonLit(floor) ? '●' : '○';
            panel.Append($"[{floor}:{light}] ");
        }
        lines.AppendLine(panel.ToString());
        string location = elevator.PositionQuarter % 4 == 0
            ? $"Etasje {elevator.PositionQuarter / 4 + 1}"
            : "Mellom etasjer";
        lines.AppendLine($"Posisjon: {location}   Dør: {(elevator.DoorOpen ? "ÅPEN" : "lukket")}"
            + $"   Mål: {(elevator.TargetFloor?.ToString() ?? "ingen")}");

        // Plasser hver rad direkte på skjermen uten WriteLine.
        // WriteLine kan få konsollen til å rulle og bildet til å hoppe.
        string[] renderedLines = lines.ToString().TrimEnd('\r', '\n').Split('\n');
        int top = Console.WindowTop;
        int rows = Math.Min(renderedLines.Length, Console.WindowHeight - 1);
        for (int index = 0; index < rows; index++)
        {
            Console.SetCursorPosition(0, top + index);

            if (index == 0)
            {
                // HEIS i cyan, skille og instruksjonstittel i hvitt.
                const string header = "HEIS | Instruksjon:";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("HEIS");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(header[4..]);
                Console.Write(new string(' ', width - header.Length));
                continue;
            }

            ConsoleColor baseColor = index == 3 && status.Length > 0
                ? ConsoleColor.Yellow
                : ConsoleColor.Gray;
            WriteColoredLine(renderedLines[index].TrimEnd('\r'), width, baseColor);
        }
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static void WriteColoredLine(string text, int width, ConsoleColor baseColor)
    {
        if (text.Length > width) text = text[..width];
        Console.ForegroundColor = baseColor;

        // Skriv vanlig tekst samlet, og fargelegg bare selve knappesymbolene.
        // Da slipper vi tusenvis av enkeltutskrifter i hver animasjonsramme.
        int start = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != '●' && text[i] != '○') continue;

            if (i > start) Console.Write(text[start..i]);
            Console.ForegroundColor = text[i] == '●' ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
            Console.Write(text[i]);
            Console.ForegroundColor = baseColor;
            start = i + 1;
        }

        if (start < text.Length) Console.Write(text[start..]);
        Console.Write(new string(' ', width - text.Length));
    }
}
