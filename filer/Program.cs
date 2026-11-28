using System.Text.Json;

namespace filer
{
    public class Person
    {
        public string Name;
        public int Age;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //string message = "Hello, World!";
            string filename = "output.txt";
            string filename2 = "output2.txt";
            string filename3 = "output3.txt";

            /*File.WriteAllText(filename, message);
            Console.WriteLine($"Saved {message} to file {filename}");*/

            // Writes string array, one per line
            /*string[] fruits = { "Apple", "Banana", "Cherry" };
            File.WriteAllLines(filename2, fruits);
            Console.WriteLine($"Saved array to file {filename2}");*/

            // Get the current directory.
            /*string currentDir = Directory.GetCurrentDirectory();
            Console.WriteLine($"The current directory is {currentDir}");*/

            // Check if file exists before reading
            /*string path = Path.Combine(currentDir, filename);
            Console.WriteLine($"File location is {path}");

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                Console.WriteLine($"File contents is: {text}");
            }
            else
            {
                Console.WriteLine("File not found!");
            }*/

            // Collections
            // List: iterate, Add, Count, Remove, RemoveAt, Contains, IndexOf, Sort, Reverse

            List<int> numbers = new List<int>(); // Empty list
            var tall = new List<int> { 4, 1, 2, 3, 9, 4, 5, 1, 7, 8, 2, 10 };  // Using var for cleaner syntax

            for (int i = 0; i < 15; i++)
            {
                numbers.Add(i);
            }

            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
            Console.WriteLine("\nAntall i liste 1: " + numbers.Count);
            int antall = numbers.Count;
            Console.WriteLine("Siste tall liste 1: " + numbers[antall - 1]);

            foreach (int t in tall)
            {
                Console.Write(t + " ");
            }
            Console.WriteLine("\nAntall i liste 2: " + tall.Count);

            tall.RemoveAt(0);
            tall.RemoveAt(2);
            tall.RemoveAt(4);
            Console.WriteLine("Antall igjen i i liste 2: " + tall.Count);
            foreach (int t in tall)
            {
                Console.Write(t + " ");
            }

            tall.Sort();
            Console.WriteLine("\nSortert liste 2: ");
            foreach (int t in tall)
            {
                Console.Write(t + " ");
            }

            // Dictionary: iterate, Add, Count, Remove, RemoveAt, Contains, IndexOf, Sort, Reverse
            // JSON
            // Serialize an object to a JSON string
            var options = new JsonSerializerOptions { IncludeFields = true };
            Person person = new Person { Name = "Alice", Age = 30 };
            string json = JsonSerializer.Serialize(person, options);

            Console.WriteLine($"\nJSON: {json}");
            File.WriteAllText(filename3, json);

            string jsonInput = File.ReadAllText(filename3);
            //Console.WriteLine($"JSON fra fil: {jsonInput}");
            Person person2 = JsonSerializer.Deserialize<Person>(jsonInput, options);
            Console.WriteLine($"JSON fra fil: {person2.Name}, {person2.Age}");
        }
    }
}

