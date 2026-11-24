namespace clicker
{
    public class Clicker
    {
        private int points;

        public Clicker()
        {
            points = 0;
        }

        public void increasePoints()
        {
            points++;
        }

        public void decreasePoints()
        {
            points--;
        }

        public void showPoints()
        {
            Console.Write(points);
        }
    }
    internal class Program
    {
        //public record Player(string Name, int Points);
        static void Main(string[] args)
        {
            // Brukeren ha tre clickere og velge hvilken som skal klikkes.

            // Instansiere objektene i arrayet
            Clicker[] clk = new Clicker[3] { new Clicker(), new Clicker(), new Clicker() };

            // Variabel for å holde styr på hvilken clicker som er valgt
            int selectedIndex = 0;

            while (true)
            {
                Console.WriteLine("""
                              Press I to increase Points
                              Press S to show Points
                              Press number 1, 2, or 3 to select a clicker
                              Press Q to quit
                              """);
                var userInput = Console.ReadLine().Trim().ToUpper();
                if (userInput == "Q")
                    break;

                // Velg clicker
                if (userInput == "1" || userInput == "2" || userInput == "3")
                {
                    selectedIndex = int.Parse(userInput) - 1; // -1 fordi array starter på 0
                    Console.WriteLine($"Selected clicker {selectedIndex + 1}");
                }
                // Øk poeng på den valgte
                else if (userInput == "I")
                {
                    clk[selectedIndex].increasePoints();
                    Console.WriteLine("Points increased!");
                }
                // Vis alle poeng
                else if (userInput == "S")
                {
                    Console.Write($"Current points: ");
                    clk[0].showPoints();
                    Console.Write($", ");
                    clk[1].showPoints();
                    Console.Write($", ");
                    clk[2].showPoints();
                    Console.WriteLine($".");
                }

            }
        }
    }
}
