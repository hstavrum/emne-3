namespace _323C
{
    //Tic-tac-toe spill
    public class Square
    {
        private bool _player1 = false;
        private bool _player2 = false;

        public bool IsEmpty()
        {
            return !_player1 && !_player2;
        }

        public bool IsPlayer1()
        {
            return _player1;
        }

        // Sjekker om det er spiller 2 (hvis ikke spiller 1)
        public bool IsPlayer2()
        {
            return _player2;
        }

        public void SetPlayer(bool isPlayer1)
        {
            if (IsEmpty())
            {
                if (isPlayer1)
                {
                    _player1 = true;
                }
                else
                {
                    _player2 = true;
                }
            }
        }
    }

    // Klasse som holder på hele brettet (9 ruter)
    public class Board
    {
        private Square[] _squares = new Square[9];
        private readonly Random _random = new Random();

        public Board()
        {
            for (int i = 0; i < 9; i++)
            {
                _squares[i] = new Square();
            }
        }

        public Square GetSquare(int index)
        {
            return _squares[index];
        }

        // Markerer en rute basert på input som "a1", "b2" osv.
        public bool Mark(string position)
        {
            if (string.IsNullOrWhiteSpace(position) || position.Length != 2)
            {
                Console.WriteLine("Ugyldig format!");
                return false;
            }

            char colChar = char.ToLowerInvariant(position[0]);

            if (!int.TryParse(position.Substring(1), out int row))
            {
                Console.WriteLine("Ugyldig rad!");
                return false;
            }

            int colIndex = colChar - 'a';
            int rowIndex = row - 1;

            if (colIndex < 0 || colIndex >= 3 ||
                rowIndex < 0 || rowIndex >= 3)
            {
                Console.WriteLine("Ugyldig koordinat!");
                return false;
            }

            int index = rowIndex * 3 + colIndex;

            if (!_squares[index].IsEmpty())
            {
                Console.WriteLine("Ruten er allerede opptatt!");
                return false;
            }

            _squares[index].SetPlayer(true);
            return true;
        }

        // Setter en tilfeldig rute til en spiller
        public void MarkRandom(bool isPlayer1)
        {
            if (IsFull()) return;

            int index;

            do
            {
                index = _random.Next(0, 9);
            }
            while (!_squares[index].IsEmpty());

            _squares[index].SetPlayer(isPlayer1);
        }

        public bool CheckWinner()
        {
            // Rader
            for (int i = 0; i < 9; i += 3)
            {
                if (IsSame(_squares[i], _squares[i + 1], _squares[i + 2])) return true;
            }
            // Kolonner
            for (int i = 0; i < 3; i++)
            {
                if (IsSame(_squares[i], _squares[i + 3], _squares[i + 6])) return true;
            }
            // Diagonaler
            if (IsSame(_squares[0], _squares[4], _squares[8])) return true;
            if (IsSame(_squares[2], _squares[4], _squares[6])) return true;

            return false;
        }

        private bool IsSame(Square s1, Square s2, Square s3)
        {
            if (s1.IsEmpty() || s2.IsEmpty() || s3.IsEmpty()) return false;
            return s1.IsPlayer1() == s2.IsPlayer1() && s2.IsPlayer1() == s3.IsPlayer1();
        }

        public bool IsFull()
        {
            foreach (var s in _squares)
            {
                if (s.IsEmpty()) return false;
            }
            return true;
        }
    }

    public class GameConsole
    {
        public void Show()
        {
            Console.WriteLine("  a b c");
            Console.WriteLine(" ┌─────┐");
            Console.WriteLine("1│     │");
            Console.WriteLine("2│     │");
            Console.WriteLine("3│     │");
            Console.WriteLine(" └─────┘");
        }

        public void Show(Board board)
        {
            Console.WriteLine("\n  a b c");
            Console.WriteLine(" ┌─────┐");
            for (int row = 0; row < 3; row++)
            {
                Console.Write($"{row + 1}│");
                for (int col = 0; col < 3; col++)
                {
                    Square sq = board.GetSquare(row * 3 + col);
                    if (sq.IsPlayer1()) Console.Write("× ");
                    else if (sq.IsPlayer2()) Console.Write("o ");
                    else Console.Write("  ");
                }
                Console.WriteLine("│");
            }
            Console.WriteLine(" └─────┘");
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Board board = new Board();
                GameConsole gameConsole = new GameConsole();

                while (true)
                {
                    gameConsole.Show(board);

                    Console.Write("Skriv inn hvor du vil sette kryss: ");
                    string position = Console.ReadLine();

                    // Player 1
                    if (!board.Mark(position))
                    {
                        continue;
                    }

                    // Check player 1
                    if (board.CheckWinner())
                    {
                        gameConsole.Show(board);
                        Console.WriteLine("Du vant!");
                        break;
                    }

                    if (board.IsFull())
                    {
                        gameConsole.Show(board);
                        Console.WriteLine("Uavgjort!");
                        break;
                    }

                    // Player 2 (computer)
                    Thread.Sleep(1000);
                    board.MarkRandom(false);

                    // Check player 2
                    if (board.CheckWinner())
                    {
                        gameConsole.Show(board);
                        Console.WriteLine("Datamaskinen vant!");
                        break;
                    }

                    if (board.IsFull())
                    {
                        gameConsole.Show(board);
                        Console.WriteLine("Uavgjort!");
                        break;
                    }
                }

                Console.Write("Spille på nytt? (j/n): ");

                if (Console.ReadLine()?.ToLowerInvariant() != "j")
                {
                    break;
                }

                Console.Clear();
            }
        }
    }
}