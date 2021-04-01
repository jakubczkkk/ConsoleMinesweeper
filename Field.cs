using System;

namespace ConsoleMinesweeper
{

    class Field
    {

        public bool IsMine { get; set; }
        public bool IsHidden { get; set; }
        public bool IsMarked { get; set; }
        public int NearbyMines { get; set; }

        public Field()
        {
            IsMine = false;
            IsHidden = true;
            NearbyMines = 0;
        }

        public void Print()
        {
            // print hidden field
            if (IsHidden)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(".");
            }

            // print marked mine
            else if (IsMarked)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("M");
            }

            // print mine
            else if (IsMine)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("X");
            }
            else
            {
                // print field without nearby mines
                if (NearbyMines == 0)
                {
                    Console.Write(" ");
                    return;
                }

                // print field with nearby mines
                ConsoleColor fieldConsoleColor; 
                switch (NearbyMines)
                {
                    case 1:
                        fieldConsoleColor = ConsoleColor.Blue;
                        break;
                    case 2:
                        fieldConsoleColor = ConsoleColor.Green;
                        break;
                    case 3:
                        fieldConsoleColor = ConsoleColor.DarkRed;
                        break;
                    case 4:
                        fieldConsoleColor = ConsoleColor.DarkMagenta;
                        break;
                    case 5:
                        fieldConsoleColor = ConsoleColor.DarkCyan;
                        break;
                    default:
                        fieldConsoleColor = ConsoleColor.Black;
                        break;
                }
                Console.ForegroundColor = fieldConsoleColor;
                Console.Write(NearbyMines);
            }
        }

    }

}
