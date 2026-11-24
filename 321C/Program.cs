namespace _321C
{
    /*
      La oss si at du skal telle opp en masse mynter. Du plukker for eksempel 10 5-kroninger og 
      legger over i en bøtte for det som er talt opp. Hver opptelling blir til et objekt i en array. 
      I JavaScript ville det blitt sånn her:

        let coinCounts = [
            {value: 1, count: 7},   // 7 stk. 1-kroninger
            {value: 5, count: 3},   // 3 stk. 5-kroninger
            {value: 10, count: 2},  // 2 stk. 10-kroninger
            {value: 10, count: 11}, // 11 stk. 10-kroninger
        ];
        Lag denne arrayen med objekter på enkleste vis i C#
        Regn ut total pengeverdi
        Gjør det mer objektorientert ved å trekke ut i metoder + flytte metodene til den klassen hver metode passer best i
        Sørg for full innkapsling      
    */


    // Representerer en type mynt og antallet av den
    public class Coin
    {
        // Private felt for full innkapsling
        private int _value;
        private int _count;

        public int Value
        {
            get => _value;
            set => _value = value;
        }

        public int Count
        {
            get => _count;
            set => _count = value;
        }

        public Coin(int value, int count)
        {
            _value = value;
            _count = count;
        }

        // Beregner verdien av myntvaløren
        public int GetTotalValue()
        {
            return _value * _count;
        }
    }

    // Representerer bøtten som holder på alle myntene
    public class CoinBucket
    {
        private readonly List<Coin> _coins = new List<Coin>();

        // Metode for å legge til nye myntopptellinger
        public void AddCoinCount(int value, int count)
        {
            _coins.Add(new Coin(value, count));
        }

        // Beregner totalverdien av alt i bøtten
        public int CalculateTotalValue()
        {
            int total = 0;
            foreach (var coin in _coins)
            {
                total += coin.GetTotalValue();
            }
            return total;
        }
    }

    class Program
    {
        static void Main()
        {
            CoinBucket bucket = new CoinBucket();

            // Legg til data
            bucket.AddCoinCount(1, 7);
            bucket.AddCoinCount(5, 3);
            bucket.AddCoinCount(10, 2);
            bucket.AddCoinCount(10, 11);

            // Beregn resultatet
            int total = bucket.CalculateTotalValue();

            Console.WriteLine($"Totalverdi i bøtten: {total} kr");
        }
    }
}
