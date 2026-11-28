namespace _329B
{
    public class VirtualScreenCell
    {
        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Left { get; private set; }
        public bool Right { get; private set; }

        public char GetCharacter()
        {
            if (Up && Down && Left && Right) return '┼';
            if (Up && Down && Left && !Right) return '┤';
            if (Up && Down && !Left && !Right) return '│';
            if (Up && Down && !Left && Right) return '├';

            if (Up && !Down && Left && !Right) return '┘';
            if (Up && !Down && Left && Right) return '┴';
            if (Up && !Down && !Left && !Right) return '╵';
            if (Up && !Down && !Left && Right) return '└';

            if (!Up && Down && Left && !Right) return '┐';
            if (!Up && Down && Left && Right) return '┬';
            if (!Up && Down && !Left && !Right) return '╷';
            if (!Up && Down && !Left && Right) return '┌';

            if (!Up && !Down && Left && !Right) return '╴';
            if (!Up && !Down && Left && Right) return '─';
            if (!Up && !Down && !Left && !Right) return ' ';
            if (!Up && !Down && !Left && Right) return '╶';

            return ' ';
        }
        public void AddHorizontal() { Left = Right = true; }
        public void AddVertical() { Up = Down = true; }
        public void AddLowerLeftCorner() { Up = Right = true; }
        public void AddLowerRightCorner() { Up = Left = true; }
        public void AddUpperLeftCorner() { Down = Right = true; }
        public void AddUpperRightCorner() { Down = Left = true; }

    }
    public class VirtualScreenRow
    {
        private VirtualScreenCell[] _cells;

        public VirtualScreenRow(int width)
        {
            _cells = new VirtualScreenCell[width];
            for (int i = 0; i < width; i++)
            {
                _cells[i] = new VirtualScreenCell();
            }
        }

        public void AddBoxTopRow(int boxX, int boxWidth)
        {
            _cells[boxX].AddUpperLeftCorner();
            for (var i = 1; i < boxWidth - 1; i++)
            {
                _cells[boxX + i].AddHorizontal();
            }
            _cells[boxX + boxWidth - 1].AddUpperRightCorner();
        }
        public void AddBoxBottomRow(int boxX, int boxWidth)
        {
            _cells[boxX].AddLowerLeftCorner();
            for (var i = 1; i < boxWidth - 1; i++)
            {
                _cells[boxX + i].AddHorizontal();
            }
            _cells[boxX + boxWidth - 1].AddLowerRightCorner();
        }
        public void AddBoxMiddleRow(int boxX, int boxWidth)
        {
            _cells[boxX].AddVertical();
            _cells[boxX + boxWidth - 1].AddVertical();
        }
        public void Show()
        {
            Console.CursorLeft = 0;
            var chars = _cells.Select(cell => cell.GetCharacter()).ToArray();
            Console.WriteLine(chars);
        }
    }

    public class Box
    {
        public int X { get; }
        public int Y { get; }
        public int StartY => Y;
        public int EndY => Y + Height;
        public int Width { get; }
        public int Height { get; }
        private int _minimumSize = 3;

        public Box(Random random, int maxX, int maxY)
        {
            Width = random.Next(_minimumSize, maxX);
            Height = random.Next(_minimumSize, maxY);
            X = random.Next(1, maxX - Width);
            Y = random.Next(1, maxY - Height);
        }
    }
    public class VirtualScreen
    {
        private VirtualScreenRow[] _rows;

        public VirtualScreen(int width, int height)
        {
            _rows = new VirtualScreenRow[height];
            for (int i = 0; i < height; i++)
            {
                _rows[i] = new VirtualScreenRow(width);
            }
        }
        public void Add(Box box)
        {
            _rows[box.StartY].AddBoxTopRow(box.X, box.Width);
            for (var i = box.StartY + 1; i < box.EndY; i++)
            {
                _rows[i].AddBoxMiddleRow(box.X, box.Width);
            }
            _rows[box.EndY].AddBoxBottomRow(box.X, box.Width);
        }
        public void Show()
        {
            Console.Clear();
            foreach (var screenRow in _rows)
            {
                screenRow.Show();
            }
        }
    }
    public class Program
    {
        private static int _width = 40;
        private static int _height = 20;

        static void Main(string[] args)
        {
            while (true)
            {
                var boxes = CreateBoxes();
                Show(boxes);
                Console.WriteLine("(press enter for new. ctrl+c=exit)");
                Console.ReadLine();
            }
            /*var cell = new VirtualScreenCell();
            cell.AddLowerLeftCorner();
            cell.AddVertical();
            Console.WriteLine(cell.GetCharacter());
            
            var row1 = new VirtualScreenRow(20);
            var row2 = new VirtualScreenRow(20);
            var row3 = new VirtualScreenRow(20);
            row1.AddBoxTopRow(5, 3);
            row2.AddBoxMiddleRow(4, 5);
            row3.AddBoxBottomRow(3, 7);
            row1.Show();
            row2.Show();
            row3.Show();*/
        }

        private static Box[] CreateBoxes()
        {
            var random = new Random();
            var boxes = new Box[3];
            for (var i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new Box(random, _width, _height);
            }
            return boxes;
        }

        private static void Show(Box[] boxes)
        {
            var screen = new VirtualScreen(_width, _height);
            foreach (var box in boxes)
            {
                screen.Add(box);
            }
            screen.Show();
        }
    }
}
