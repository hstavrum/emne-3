// Classic Breakout – konsollspill i C#

const int bredde = 50;
const int hoyde = 20;
const int plateBredde = 7;
const int antallMursteiner = 10;
const int mursteinBredde = 4;
const int mursteinY = 3;

int plateX = (bredde - plateBredde) / 2;
int plateY = hoyde - 2;

int ballX = bredde / 2;
int ballY = hoyde / 2;
int fartX = 1;
int fartY = -1;

bool[] mursteiner = new bool[antallMursteiner];

for (int i = 0; i < mursteiner.Length; i++)
{
    mursteiner[i] = true;
}

Console.CursorVisible = false;
Console.Clear();

TegnVegger(bredde, hoyde);

bool spiller = true;
bool vunnet = false;

while (spiller)
{
    // Fjern den gamle ballen og plata
    TegnBall(ballX, ballY, ' ', ConsoleColor.White);
    TegnPlate(plateX, plateY, plateBredde, ' ');

    // Les tastetrykk
    if (Console.KeyAvailable)
    {
        ConsoleKey tast = Console.ReadKey(true).Key;

        if (tast == ConsoleKey.LeftArrow && plateX > 1)
        {
            plateX -= 4;
        }

        if (tast == ConsoleKey.RightArrow &&
            plateX + plateBredde < bredde - 1)
        {
            plateX += 4;
        }
    }

    // Flytt ballen
    int nesteX = ballX + fartX;
    int nesteY = ballY + fartY;

    // Sprett mot sideveggene
    if (nesteX <= 0 || nesteX >= bredde - 1)
    {
        fartX = -fartX;
        nesteX = ballX + fartX;
    }

    // Sprett mot taket
    if (nesteY <= 0)
    {
        fartY = -fartY;
        nesteY = ballY + fartY;
    }

    // Sjekk mursteinene
    if (fartY < 0 && nesteY == mursteinY)
    {
        for (int i = 0; i < mursteiner.Length; i++)
        {
            int mursteinX = 2 + i * (mursteinBredde + 1);

            if (mursteiner[i] &&
                nesteX >= mursteinX &&
                nesteX < mursteinX + mursteinBredde)
            {
                mursteiner[i] = false;

                TegnMurstein(mursteinX, mursteinY,
                             mursteinBredde, ' ');

                fartY = -fartY;
                nesteY = ballY + fartY;
                break;
            }
        }
    }

    // Sprett mot plata
    if (fartY > 0 && nesteY == plateY &&
        nesteX >= plateX &&
        nesteX < plateX + plateBredde)
    {
        fartY = -fartY;
        nesteY = ballY + fartY;
    }

    // Tap dersom ballen går forbi plata
    if (nesteY >= hoyde - 1)
    {
        spiller = false;
    }
    else
    {
        ballX = nesteX;
        ballY = nesteY;
    }

    // Sjekk om alle mursteinene er borte
    bool alleBorte = true;

    for (int i = 0; i < mursteiner.Length; i++)
    {
        if (mursteiner[i])
        {
            alleBorte = false;
        }
    }

    if (alleBorte)
    {
        vunnet = true;
        spiller = false;
    }

    // Tegn spillet
    for (int i = 0; i < mursteiner.Length; i++)
    {
        if (mursteiner[i])
        {
            int mursteinX = 2 + i * (mursteinBredde + 1);
            TegnMurstein(mursteinX, mursteinY,
                         mursteinBredde, '#');
        }
    }

    TegnPlate(plateX, plateY, plateBredde, '=');

    if (spiller)
    {
        TegnBall(ballX, ballY, 'O', ConsoleColor.Yellow);
    }

    Thread.Sleep(100);
}

// Vis resultatet
Console.ResetColor();
Console.SetCursorPosition(0, hoyde + 1);

if (vunnet)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Gratulerer! Du vant!");
}
else
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Game Over! Du tapte!");
}

Console.ResetColor();
Console.CursorVisible = true;


// ---------- STATISKE METODER ---------- 

static void TegnVegger(int bredde, int hoyde)
{
    Console.ForegroundColor = ConsoleColor.DarkGray;

    for (int x = 0; x < bredde; x++)
    {
        Console.SetCursorPosition(x, 0);
        Console.Write('#');
    }

    for (int y = 1; y < hoyde; y++)
    {
        Console.SetCursorPosition(0, y);
        Console.Write('#');

        Console.SetCursorPosition(bredde - 1, y);
        Console.Write('#');
    }
}

static void TegnBall(int x, int y, char tegn,
                     ConsoleColor farge)
{
    Console.ForegroundColor = farge;
    Console.SetCursorPosition(x, y);
    Console.Write(tegn);
}

static void TegnPlate(int x, int y, int bredde, char tegn)
{
    Console.ForegroundColor = ConsoleColor.Cyan;

    for (int i = 0; i < bredde; i++)
    {
        Console.SetCursorPosition(x + i, y);
        Console.Write(tegn);
    }
}

static void TegnMurstein(int x, int y, int bredde, char tegn)
{
    Console.ForegroundColor = ConsoleColor.Red;

    for (int i = 0; i < bredde; i++)
    {
        Console.SetCursorPosition(x + i, y);
        Console.Write(tegn);
    }
}
