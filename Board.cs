using System;
using System.Text;

namespace ConsoleMinesweeper
{

    class Board
    {

        public static int Width = 9;
        public static int Height = 9;
        public static int NumerOfMines = 10;

        public int CurrentMines;

        public Field[,] Fields = new Field[Width, Height];

        public Board()
        {
            for (var i = 0; i < Width; ++i)
            {
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i,j] = new Field();
                }
            }

            int remainingMines = NumerOfMines;
            CurrentMines = remainingMines;
            Random rnd = new Random();
            while (remainingMines > 0)
            {
                int x = rnd.Next(Width);
                int y = rnd.Next(Height);

                if (!Fields[x,y].IsMine)
                {
                    Fields[x, y].IsMine = true;
                    remainingMines--;
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
                GameState.IsPlaying = false;
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
                                Console.WriteLine($"Odkrywamy {i+1} {j+1}");
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

        public void PrintBoard()
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
                Console.Write(" | ");
                for (var j = 0; j < Height; ++j)
                {
                    Console.Write(Fields[i,j]);
                    Console.Write(" ");
                }
                Console.WriteLine("|");

            }

            Console.WriteLine(horizontalBorder.ToString());

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
            PrintBoard();
        }

    }

}