namespace StampCardApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Skriv navn: ");
                string ownerName = Console.ReadLine();

                // Er navnet tomt, kaster konstruktøren en ArgumentException.
                StampCard card = new StampCard(ownerName);

                for (int i = 0; i < 10; i++)
                {
                    card.AddStamp();
                }

                if (card.RedeemFreeCoffee())
                {
                    Console.WriteLine("Gratis kaffe til: {0}", card.Owner);
                }
            }
            catch (ArgumentException ex)
            {

                Console.WriteLine($"Feil: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Fange evt. andre feil
                Console.WriteLine($"En uventet feil oppstod: {ex.Message}");
            }
        }
    }
}
