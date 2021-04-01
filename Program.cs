using System;
using static ConsoleMinesweeper.GameState;

namespace ConsoleMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {

            // initiate board
            var board = new Board();
            
            while (board.CurrentGameState == InGame)
            {
                Console.Clear();
                board.Print();

                Console.WriteLine();
                Console.WriteLine(
                    $"Number of unchecked mines: {board.CurrentMines}");
                Console.WriteLine("Enter coordinates (row, column): ");

                // getting input from user
                try
                {
                    // array input contains chosen by user coordinates (index 0, 1)
                    // and optional parameter used when user wants to mark field as mine (index 2)
                    string[] input = Console.ReadLine().Split();
                    int x = int.Parse(input[0]);
                    int y = int.Parse(input[1]);
                    if (input.Length == 3 && input[2].ToLower() == "m")
                    {
                        board.MarkMine(x, y);
                    }
                    else
                    {
                        board.CheckField(x, y);
                    }
                }
                catch
                {
                    continue;
                }

                if (board.CurrentMines == 0)
                {
                    board.CurrentGameState = Won;
                }

            }

            Console.Clear();
            board.ShowAllFields();

            Console.WriteLine();
            if (board.CurrentGameState == Won)
            {
                Console.WriteLine("You won!");
            }
            else if (board.CurrentGameState == Lost)
            {
                Console.WriteLine("You lost! :(");
            }

            Console.WriteLine("Press any key to play again!");
            Console.ReadKey();
            Program.Main(args);

        }
    }

    public enum GameState
    {
        InGame,
        Lost,
        Won
    }
}
