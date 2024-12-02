using System.Collections.Generic;
using System.Threading;
using System;

class Program

{
    private static int screenwidth;
    private static int screenheight;
    private static string score;

    static void Main(string[] args)

    {

        Console.WindowHeight = 16;

        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;

        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        Pixel hoofd = new Pixel
        {
            xPos = screenwidth / 2,

            yPos = screenheight / 2,

            SchermKleur = ConsoleColor.Red
        };
        string movement = "RIGHT";

        List<int> telje = new List<int>();

        int score = 0;

        List<int> teljePositie = new List<int>();



        teljePositie.Add(hoofd.xPos);

        teljePositie.Add(hoofd.yPos);



        DateTime tijd = DateTime.Now;

        string obstacle = "*";

        int obstacleXpos = randomnummer.Next(1, screenwidth);

        int obstacleYpos = randomnummer.Next(1, screenheight);

        while (true)

        {


            for (int i = 0; i < telje.Count; i++)

            {

                Console.SetCursorPosition(telje[i], telje[i + 1]);

                Console.Write("■");

            }


            ConsoleKeyInfo info = Console.ReadKey();

            //Game Logic

            switch (info.Key)

            {

                case ConsoleKey.UpArrow:

                    movement = "UP";

                    break;

                case ConsoleKey.DownArrow:

                    movement = "DOWN";

                    break;

                case ConsoleKey.LeftArrow:

                    movement = "LEFT";

                    break;

                case ConsoleKey.RightArrow:

                    movement = "RIGHT";

                    break;

            }

            if (movement == "UP")

                hoofd.yPos--;

            if (movement == "DOWN")

                hoofd.yPos++;

            if (movement == "LEFT")

                hoofd.xPos--;

            if (movement == "RIGHT")

                hoofd.xPos++;

            //Hindernis treffen

            if (hoofd.xPos == obstacleXpos || hoofd.yPos == obstacleYpos)

            {

                score++;

                obstacleXpos = randomnummer.Next(1, screenwidth);

                obstacleYpos = randomnummer.Next(1, screenheight);

            }

            teljePositie.Insert(0, hoofd.xPos);

            teljePositie.Insert(1, hoofd.yPos);

            teljePositie.RemoveAt(teljePositie.Count - 1);

            teljePositie.RemoveAt(teljePositie.Count - 1);

            //Kollision mit Wände oder mit sich selbst

            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 || hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)

            {
                AffterGame();

            }

            for (int i = 0; i < telje.Count; i += 2)

            {

                if (hoofd.xPos == telje[i] && hoofd.yPos == telje[i + 1])

                {

                    AffterGame();

                }

            }

            Thread.Sleep(50);

        }

    }
    static void AffterGame()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red;

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2);

        Console.WriteLine("Game Over");

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);

        Console.WriteLine("Dein Score ist: " + score);

        Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);

        Environment.Exit(0);
    }
    static void DrawBorders()
    {
        Console.ForegroundColor = ConsoleColor.White;

        string horizontalBorder = new string('■', screenwidth);
        Console.SetCursorPosition(0, 0);
        Console.Write(horizontalBorder);
        Console.SetCursorPosition(0, screenheight - 1);
        Console.Write(horizontalBorder);

        for (int i = 1; i < screenheight - 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(screenwidth - 1, i);
            Console.Write("■");
        }
    }
}