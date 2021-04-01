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
        public int CurrentMines { get; set; }
        public GameState CurrentGameState { get; set; }
        public Field[,] Fields { get; set; }

        public Board()
        {   
            // initiate board's properties
            Fields = new Field[Width, Height];
            CurrentGameState = InGame;
            CurrentMines = NumerOfMines;
            for (var i = 0; i < Width; ++i)
            {
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i,j] = new Field();
                }
            }

            // place mines on board
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

            // user checks board with mine -> game lost
            if (Fields[x, y].IsMine)
            {
                CurrentGameState = Lost;
            }

            // show all nearby fields without neighbouring mines
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
                        catch 
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

            // print top coordinates
            StringBuilder coordinates = new StringBuilder();
            coordinates.Append("    ");
            for (var i = 1; i <= Width; ++i)
            {
                coordinates.Append(i);
                coordinates.Append(" ");
            }
            Console.WriteLine(coordinates.ToString());

            // print top border
            StringBuilder border = new StringBuilder();
            border.Append("   ");
            for (var i = 0; i < 2 * Width + 1; ++i)
            {
                border.Append("-");
            }
            Console.WriteLine(border.ToString());

            for (var i = 0; i < Width; ++i)
            {
                // print left coordinates
                Console.Write(i+1);

                // print left border
                Console.Write(" |");

                // print fields
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" ");
                for (var j = 0; j < Height; ++j)
                {
                    Fields[i,j].Print();
                    Console.Write(" ");
                }
                Console.ResetColor();

                // print right border
                Console.Write("| ");

                //print right coordinates
                Console.Write(i+1);
                Console.WriteLine();

            }

            // print bottom border
            Console.WriteLine(border.ToString());

            // print bottom coordinates
            Console.WriteLine(coordinates.ToString());

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
                    catch 
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
