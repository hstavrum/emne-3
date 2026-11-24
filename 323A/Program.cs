namespace _323A
{
    static class Match
    {
        public static int HomeGoals { get; set; }
        public static int AwayGoals { get; set; }
        public static bool IsRunning { get; set; } = true;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Gyldig tips: \n" +
                " - H, U, B\n" +
                " - halvgardering: HU, HB, UB\n" +
                " - helgardering: HUB\n" +
                "Hva har du tippet for denne kampen? ");
            var bet = Console.ReadLine();

            Match.HomeGoals = 0;
            Match.AwayGoals = 0;
            Match.IsRunning = true;

            while (Match.IsRunning)
            {
                Console.Write("Kommandoer: \n" +
                    " - H = scoring hjemmelag\n" +
                    " - B = scoring bortelag\n" +
                    " - X = kampen er ferdig\n" +
                    "Angi kommando: ");
                var command = Console.ReadLine();

                Match.IsRunning = command != "X";

                if (command == "H") Match.HomeGoals++;
                else if (command == "B") Match.AwayGoals++;
                Console.WriteLine($"Stillingen er {Match.HomeGoals}-{Match.AwayGoals}.");
            }

            var result = Match.HomeGoals == Match.AwayGoals ? "U" : Match.HomeGoals > Match.AwayGoals ? "H" : "B";
            var isBetCorrect = bet.Contains(result);
            var isBetCorrectText = isBetCorrect ? "riktig" : "feil";
            Console.WriteLine($"Du tippet {isBetCorrectText}");
        }
    }
}
