using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static int screenwidth;
    private static int screenheight;
    private static int score;

    static void Main(string[] args)
    {
        screenwidth = Console.WindowWidth = 32;
        screenheight = Console.WindowHeight = 16;

        Random randomnummer = new Random();

        Pixel hoofd = new Pixel
        {
            xPos = screenwidth / 2,
            yPos = screenheight / 2,
            SchermKleur = ConsoleColor.Green
        };

        string movement = "RIGHT";
        List<int> snakeBody = new List<int> { hoofd.xPos, hoofd.yPos };

        int obstacleXpos = randomnummer.Next(1, screenwidth - 1);
        int obstacleYpos = randomnummer.Next(1, screenheight - 1);

       
        DrawBorders();

        while (true)
        {
            
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: if (movement != "DOWN") movement = "UP"; break;
                    case ConsoleKey.DownArrow: if (movement != "UP") movement = "DOWN"; break;
                    case ConsoleKey.LeftArrow: if (movement != "RIGHT") movement = "LEFT"; break;
                    case ConsoleKey.RightArrow: if (movement != "LEFT") movement = "RIGHT"; break;
                }
            }

           
            switch (movement)
            {
                case "UP": hoofd.yPos--; break;
                case "DOWN": hoofd.yPos++; break;
                case "LEFT": hoofd.xPos--; break;
                case "RIGHT": hoofd.xPos++; break;
            }

            
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth - 1);
                obstacleYpos = randomnummer.Next(1, screenheight - 1);

                
                snakeBody.Add(-1); 
                snakeBody.Add(-1);
            }

            
            snakeBody.Insert(0, hoofd.xPos);
            snakeBody.Insert(1, hoofd.yPos);

            
            if (snakeBody.Count > score * 2 + 2)
            {
                int tailX = snakeBody[snakeBody.Count - 2];
                int tailY = snakeBody[snakeBody.Count - 1];

                if (tailX >= 0 && tailY >= 0 && tailX < screenwidth && tailY < screenheight)
                {
                    Console.SetCursorPosition(tailX, tailY);
                    Console.Write(" ");
                }

                snakeBody.RemoveAt(snakeBody.Count - 1);
                snakeBody.RemoveAt(snakeBody.Count - 1);
            }

            
            if (hoofd.xPos == 0 || hoofd.xPos == screenwidth - 1 ||
                hoofd.yPos == 0 || hoofd.yPos == screenheight - 1)
            {
                AfterGame();
            }

            
            for (int i = 2; i < snakeBody.Count; i += 2)
            {
                if (hoofd.xPos == snakeBody[i] && hoofd.yPos == snakeBody[i + 1])
                {
                    AfterGame();
                }
            }

            
            UpdateGame(hoofd, obstacleXpos, obstacleYpos, snakeBody);

            
            Thread.Sleep(100);
            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth - 1);
                obstacleYpos = randomnummer.Next(1, screenheight - 1);

          
                snakeBody.Add(snakeBody[snakeBody.Count - 2]); 
                snakeBody.Add(snakeBody[snakeBody.Count - 1]); 
            }

        }
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

    
    static void UpdateGame(Pixel hoofd, int obstacleXpos, int obstacleYpos, List<int> snakeBody)
    {
       
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(obstacleXpos, obstacleYpos);
        Console.Write("*");

      
        Console.ForegroundColor = ConsoleColor.Green;
        for (int i = 0; i < snakeBody.Count; i += 2)
        {
            Console.SetCursorPosition(snakeBody[i], snakeBody[i + 1]);
            Console.Write("■");
        }

      
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.SetCursorPosition(2, screenheight - 1);
        Console.Write($"Score: {score}");
    }

    
    static void AfterGame()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(screenwidth / 2 - 5, screenheight / 2);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(screenwidth / 2 - 5, screenheight / 2 + 1);
        Console.WriteLine($"Score: {score}");
        Environment.Exit(0);
    }
}

