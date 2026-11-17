namespace _316A
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("ordliste.txt");

            string[] words = RetreiveWords(content);
            //Console.WriteLine(string.Join(", ", words));

            // velg et tilfeldig ord fra listen
            Random rand = new Random();
            int index = rand.Next(words.Length);
            Console.WriteLine("Tilfeldig ord: " + words[index]);

            // se om det finnes et annet ord som begynner på de 5 samme bokstavene som det tilfeldige ordet slutter på.
            string lastFiveLetters = words[index].Substring(words[index].Length - 5);
            Console.WriteLine("Siste fem bokstaver: " + lastFiveLetters);

            int y = 0;
            bool found = false;
            foreach (string word in words)
            {                
                if (word.StartsWith(lastFiveLetters) && word != words[index])
                {
                    y++;
                    Console.WriteLine(y + ". match: " + word);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ingen ord funnet som begynner på de siste fem bokstavene.");
            }
        }

        static string[] RetreiveWords(string[] content)
        {
            List<string> list = new List<string>();
            string previousWord = "";
            foreach (string line in content)
            {
                string[] parts = line.Split('\t');
                if (parts.Length >= 3)
                {
                    // oppslagsordet har plassering 3 i arrayen (indeks 2)
                    string word = parts[2];

                    // ikke skriv ut ordet hvis det er det samme som forrige
                    // ikke ta med ord som inneholder bindestrek.
                    if (word != previousWord && !word.Contains('-'))
                    {
                        // ta bare med ord med lengde på 7, 8, 9 eller 10.
                        if (word.Length > 6 && word.Length < 11) 
                        {
                            list.Add(word);
                        }                        
                    }
                    previousWord = word;
                }
            }
            return list.ToArray();
        }
    }
}
