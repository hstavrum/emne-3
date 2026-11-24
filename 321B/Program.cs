namespace _321B
{
    public class Produkt
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        // Overstyrer ToString for å gjøre det enkelt å skrive ut objektet
        public override string ToString()
        {
            return $"Navn: {ProductName} | Kategori: {Category} | Pris: {Price} kr";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Bruk en array av objekter - la brukeren skrive inn en verdi - og så vises
            // alle objekter som inneholder denne verdien i et av feltene.
            Produkt[] produkter = new Produkt[]
            {
                new Produkt { ProductName = "Laptop", Category = "Elektronikk", Price = 25000 },
                new Produkt { ProductName = "Mobil", Category = "Elektronikk", Price = 10000 },
                new Produkt { ProductName = "Bok", Category = "Hefte", Price = 200 },
                new Produkt { ProductName = "Bok", Category = "Innbundet", Price = 500 }
            };

            Console.Write("Skriv inn søkeverdi: ");
            string searchValue = Console.ReadLine();

            bool found = false;
            Console.WriteLine("\nResultater:");

            foreach (Produkt p in produkter)
            {
                if (p.ProductName.Contains(searchValue) || p.Category.Contains(searchValue))
                {
                    // Console.WriteLine method calls ToString
                    Console.WriteLine(p);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Ingen produkter funnet for: {searchValue}");
            }
        }
    }
}
