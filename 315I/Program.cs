namespace _315I
{
    internal class Program
    {
        static readonly Random Random = new Random();        
        static void Main(string[] args)
        {
            if (!IsValid(args))
            {
                ShowHelpText();
                return;
            }

            int pwLenght = int.Parse(args[0]);
            string pattern = args[1];

            // Sørger for mønsteret angitt i (args[1]) blir like langt som . Sørg for at den blir like lang som ønsket
            // passordlengde (args[0]) ved å fylle opp med små L-er.
            if (pattern.Length < pwLenght)
            {
                pattern = pattern.PadRight(pwLenght, 'l');
            }

            int i = 0;
            while (i < pattern.Length) {                
                switch (pattern[i])
                {
                    case 'l':
                        Console.Write(WriteRandomLowerCaseLetter());
                        break;
                    case 'L':
                        Console.Write(WriteRandomUpperCaseLetter());
                        break;
                    case 'd':
                        Console.Write(WriteRandomDigit());
                        break;
                    case 's':
                        Console.Write(WriteRandomSpecialCharacter());
                        break;
                }
                i++;
            }
        }

        static char WriteRandomLowerCaseLetter()
        {
            return GetRandomLetter('a', 'z');
        }

        static char WriteRandomUpperCaseLetter()
        {
            return GetRandomLetter('A', 'Z');
        }

        static char WriteRandomDigit()
        {
            return GetRandomLetter('0', '9');
        }

        static char WriteRandomSpecialCharacter()
        {
            string specialCharacters = "!\"#¤%&/(){}[]";
            return specialCharacters[Random.Next(specialCharacters.Length)];
        }

        static bool IsValid(string[] args) {
            if (args.Length == 0)
            {
                return false;   
            }

            if (args.Length > 0)
            {
                // Parse the first argument
                if (!int.TryParse(args[0], out int result))
                {
                    return false;
                }

            }

            if (!(args.Length > 1 && !string.IsNullOrEmpty(args[1])))
            {
                return false;
            }

            foreach (char c in args[1])
            {
                if (!"lLds".Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        static char GetRandomLetter(char min, char max)
        {
            return (char)Random.Next(min, max);
        }

        static void ShowHelpText() {
            Console.WriteLine("PasswordGenerator\r\nOptions:\r\n- l = liten bokstav\r\n- L = stor bokstav\r\n- d = siffer\r\n- s = spesialtegn (!\"#¤%&/(){}[]\r\nEksempel: PasswordGenerator 14 lLssdd\r\n    betyr\r\n        Min. 1 liten bokstav\r\n        Min. 1 1 stor bokstav\r\n        Min. 2 spesialtegn\r\n        Min. 2 sifre\r\n        Lengde på passordet skal være 14");
        }
    }
}
