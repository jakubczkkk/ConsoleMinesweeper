using System;
using System.Text;
using static ConsoleMinesweeper.GameState;

namespace ConsoleMinesweeper
{

    class Board
    {

        public static int Width = 9;
        public static int Height = 9;
        public static int NumerOfMines = 10;
        public int CurrentMines;
        public GameState CurrentGameState;

        public Field[,] Fields = new Field[Width, Height];

        public Board()
        {
            CurrentGameState = InGame;
            CurrentMines = NumerOfMines;
            for (var i = 0; i < Width; ++i)
            {
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i,j] = new Field();
                }
            }

            Random rnd = new Random();
            int remainingMinesToPlace = NumerOfMines;
            while (remainingMinesToPlace > 0)
            {
                int x = rnd.Next(Width);
                int y = rnd.Next(Height);

                if (!Fields[x,y].IsMine)
                {
                    Fields[x, y].IsMine = true;
                    remainingMinesToPlace--;
                    IncrementNearbyMinesCount(x, y);
                }
                else
                {
                    continue;
                }
            }

        }

        public void CheckField(int x, int y)
        {
            x--; y--;

            if (Fields[x, y].IsMine)
            {
                CurrentGameState = Lost;
            }
            else if (Fields[x, y].NearbyMines == 0)
            {
                Fields[x, y].IsHidden = false;
                for (var i = x - 1; i <= x + 1; ++i)
                {
                    for (var j = y - 1; j <= y + 1; ++j)
                    {
                        try
                        {
                            if (Fields[i, j].IsHidden && !(i == x && j== y))
                            {
                                CheckField(i + 1, j + 1);
                            }
                        }
                        catch (System.Exception)
                        {
                            continue;
                        }
                    }
                }
            }
            else 
            {
                Fields[x, y].IsHidden = false;
            }
        }

        public void Print()
        {

            StringBuilder widthCoordinates = new StringBuilder();
            widthCoordinates.Append("    ");
            for (var i = 1; i <= Width; ++i)
            {
                widthCoordinates.Append(i);
                widthCoordinates.Append(" ");
            }
            Console.WriteLine(widthCoordinates.ToString());

            StringBuilder horizontalBorder = new StringBuilder();
            horizontalBorder.Append("   ");
            for (var i = 0; i < 2 * Width + 1; ++i)
            {
                horizontalBorder.Append("-");
            }

            Console.WriteLine(horizontalBorder.ToString());

            for (var i = 0; i < Width; ++i)
            {
                Console.Write(i+1);
                Console.Write(" |");

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" ");
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i,j].Print();
                    Console.Write(" ");
                }
                Console.ResetColor();
                Console.Write("| ");
                Console.Write(i+1);
                Console.WriteLine();

            }

            Console.WriteLine(horizontalBorder.ToString());
            Console.WriteLine(widthCoordinates.ToString());

        }

        public void IncrementNearbyMinesCount(int x, int y)
        {

            for (var i = x - 1; i <= x + 1; ++i)
            {
                for (var j = y - 1; j <= y + 1; ++j)
                {
                    
                    try
                    {
                        Fields[i, j].NearbyMines += 1;
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }

                }
            }

        }

        public void ShowAllFields()
        {
            for (var i = 0; i < Width; ++i)
            {
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i, j].IsHidden = false;
                }
            }
            Print();
        }

        public void MarkMine(int x, int y)
        {

            x--; y--;
            
            if (!Fields[x, y].IsMine)
            {
                CurrentGameState = Lost;   
            }
            else
            {
                CurrentMines--;
                Fields[x, y].IsMarked = true;
                Fields[x, y].IsHidden = false;
            }

        }

    }

}