// installer Spectre.Console med "dotnet add package Spectre.Console"
namespace Kortstokken
{
    // Lag en kortstokk med 52 kort.Regler:
    // Et kort kan ikke endres etter at det er laget.
    // En ny kortstokk har nøyaktig 52 kort, uten duplikater.
    // Listen med kort skal være private. Utenfra kan man bare bruke metodene til kortstokken.

    // Kjerne:
    // Lag enum Suit (Hearts, Diamonds, Clubs, Spades) og enum Rank (Two til Ace).
    // Lag klassen Card med Suit og Rank.De skal kunne leses, men ikke endres.
    // Lag klassen Deck.Konstruktøren lager alle 52 kortene. Tips: to løkker inni hverandre.
    // Lag Draw() i Deck.Den fjerner øverste kort og skriver det ut i konsollen. Er kortstokken tom,
    //  skriver den «Det er ingen kort igjen».
    // Vis i Program.cs at alt virker: lag en kortstokk, og trekk kort til den er tom.

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            // To løkker for å lage alle 52 kombinasjonene
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Draw()
        {
            if (cards.Count == 0)
            {
                Console.WriteLine("Ingen kort igjen");
            }
            else
            {
                // Fjerner første kortet i listen
                Card drawnCard = cards[0];

                if (drawnCard.Suit == Suit.Diamonds)
                    Console.WriteLine($"Trakk: {drawnCard}");

                cards.RemoveAt(0);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Deck cardDeck = new Deck();

            // Trekk kort til stokken er tom
            for (int i = 0; i < 53; i++)
            {
                cardDeck.Draw();
            }
        }
    }
}
