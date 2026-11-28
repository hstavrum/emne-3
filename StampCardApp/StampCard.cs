namespace StampCardApp
{
    public class StampCard
    {
        // Hvilke verdier må kortet huske? Eier, antall stempler.
        public string Owner { get; }
        private int StampCount;

        public StampCard(string ownerName)
        {
            // Lager et kort med eiernavn og 0 stempler.
            if (string.IsNullOrEmpty(ownerName))
            {
                throw new ArgumentException("Navn må oppgis.");
            }

            Owner = ownerName;
            StampCount = 0;
        }

        public void AddStamp()
        {
            // Gir kortet ett stempel.
            StampCount++;
        }
        public bool RedeemFreeCoffee()
        {
            // Returnerer true og trekker 10 stempler hvis kortet har minst 10.
            if (StampCount >= 10)
            {
                StampCount -= 10;
                return true;
            }
            return false;
        }
    }
}
