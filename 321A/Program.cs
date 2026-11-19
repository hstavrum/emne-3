namespace _321A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the CharCounter class to keep track of character frequencies
            var counter = new CharCounter();

            // Initialize a variable to hold the user's input
            string text = "something";

            // If the input is not null/empty, process it by adding it to the counter
            while (!string.IsNullOrWhiteSpace(text))
            {
                text = Console.ReadLine();
                counter.AddText(text);
                counter.ShowCounts();
            }
        }
    }
}
