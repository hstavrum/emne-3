using System;
using System.Text;
using System.Threading;

static class MyConsole
{
    public static void ClearAt(int x, int y)
    {
        if (!TrySetCursor(x, y))
        {
            return;
        }
        Console.Write("   ");
    }

    public static void ClearBlock(int x, int y, int width, int height)
    {
        for (int row = 0; row < height; row++)
        {
            if (!TrySetCursor(x, y + row))
            {
                continue;
            }
            for (int i = 0; i < width; i++)
            {
                Console.Write(" ");
            }
        }
    }

    public static void WriteAt(int x, int y, string text, ConsoleColor color)
    {
        if (!TrySetCursor(x, y))
        {
            return;
        }
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    static bool TrySetCursor(int x, int y)
    {
        try
        {
            Console.SetCursorPosition(x, y);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

public static class Program
{
    const int BirdStartX = 8;
    const int GoalX = 44;
    const int GapHeight = 6;
    const int Gravity = 1;
    const int FlapPower = 3;
    const int MoveEvery = 2;
    const int SleepMs = 50;

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        int screenH = 24;
        int screenW = 80;
        try
        {
            screenH = Console.WindowHeight;
            screenW = Console.WindowWidth;
        }
        catch (Exception)
        {
            screenH = 24;
            screenW = 80;
        }
        if (screenH < 18) screenH = 18;
        if (screenW < 60) screenW = 60;

        int groundRow = screenH - 2;
        int gapTop = groundRow - 9;
        if (gapTop < 4) gapTop = 4;

        bool running = true;
        bool launched = false;
        bool over = false;
        int birdX = BirdStartX;
        int birdY = gapTop + 2;
        int prevX = birdX;
        int prevY = birdY;
        int tick = 0;

        try
        {
            Console.Clear();
        }
        catch (Exception)
        {
        }
        DrawScenery(screenW, groundRow, gapTop);

        while (running)
        {
            if (KeyPressed(out ConsoleKeyInfo key))
            {
                if (key.Key == ConsoleKey.Escape)
                {
                    running = false;
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    if (!launched)
                    {
                        launched = true;
                        birdY = gapTop + 2;
                        MyConsole.WriteAt(2, 2, "                                        ", ConsoleColor.Gray);
                    }
                    else if (!over)
                    {
                        birdY = Flap(birdY);
                    }
                }
            }

            if (!over)
            {
                StepFlight(ref birdX, ref birdY, ref tick, launched);

                if (launched)
                {
                    MyConsole.ClearBlock(prevX - 1, prevY - 1, 4, 3);
                    DrawGoal(gapTop, groundRow);
                }
                MyConsole.WriteAt(birdX, birdY, "><", ConsoleColor.Yellow);
                prevX = birdX;
                prevY = birdY;

                string result = Outcome(birdX, birdY, groundRow, gapTop, launched);
                if (result != "")
                {
                    over = true;
                    MyConsole.ClearBlock(birdX - 1, birdY - 1, 4, 3);
                    DrawScenery(screenW, groundRow, gapTop);
                    MyConsole.WriteAt(birdX, birdY, "><", ConsoleColor.Yellow);
                    bool hit = result.StartsWith("Treff!");
                    MyConsole.WriteAt(2, 2, result, hit ? ConsoleColor.Green : ConsoleColor.Red);
                }
            }

            Thread.Sleep(SleepMs);
        }

        Console.WriteLine();
        Console.WriteLine("Spillet er avsluttet.");
    }

    static bool KeyPressed(out ConsoleKeyInfo key)
    {
        key = default(ConsoleKeyInfo);
        try
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }
            key = Console.ReadKey(true);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static int Flap(int birdY)
    {
        return birdY - FlapPower;
    }

    public static void StepFlight(ref int birdX, ref int birdY, ref int tick, bool launched)
    {
        if (!launched)
        {
            return;
        }
        tick++;
        birdY += Gravity;
        if (tick % MoveEvery == 0)
        {
            birdX += 1;
        }
        if (birdY < 1)
        {
            birdY = 1;
        }
    }

    public static string Outcome(int birdX, int birdY, int groundRow, int gapTop, bool launched)
    {
        if (!launched)
        {
            return "";
        }
        if (birdY >= groundRow)
        {
            return "Miss! Du falt ned. Trykk Esc for å avslutte.";
        }
        if (birdX >= GoalX - 1)
        {
            if (HitsGoal(birdY, gapTop))
            {
                return "Treff! Du traff målet. Trykk Esc for å avslutte.";
            }
            return "Miss! Du fløy forbi målet. Trykk Esc for å avslutte.";
        }
        return "";
    }

    public static bool HitsGoal(int birdY, int gapTop)
    {
        return birdY >= gapTop && birdY <= gapTop + GapHeight - 1;
    }

    static void DrawGoal(int gapTop, int groundRow)
    {
        for (int row = 1; row <= gapTop - 1; row++)
        {
            MyConsole.WriteAt(GoalX, row, "|||", ConsoleColor.Cyan);
        }
        for (int row = gapTop + GapHeight; row <= groundRow - 1; row++)
        {
            MyConsole.WriteAt(GoalX, row, "|||", ConsoleColor.Cyan);
        }
        MyConsole.WriteAt(GoalX - 4, gapTop - 2, "MÅL", ConsoleColor.Cyan);
    }

    static void DrawScenery(int screenW, int groundRow, int gapTop)
    {
        string top = "";
        for (int i = 0; i < screenW; i++)
        {
            top += "-";
        }
        MyConsole.WriteAt(0, 0, top, ConsoleColor.DarkGray);

        string ground = "";
        for (int i = 0; i < screenW; i++)
        {
            ground += "=";
        }
        MyConsole.WriteAt(0, groundRow, ground, ConsoleColor.DarkGray);

        DrawGoal(gapTop, groundRow);

        MyConsole.WriteAt(2, 2, "Mellomrom = start skuddet, Esc = avslutt", ConsoleColor.Gray);
    }
}
