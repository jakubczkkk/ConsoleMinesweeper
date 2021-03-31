using System;
using static ConsoleMinesweeper.GameState;

namespace ConsoleMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {

            var board = new Board();
            
            while (board.CurrentGameState == InGame)
            {
                Console.Clear();
                board.Print();

                Console.WriteLine();
                Console.WriteLine(
                    $"Number of unchecked mines: {board.CurrentMines}");
                Console.WriteLine("Enter coordinates (row, column): ");

                string[] input = Console.ReadLine().Split();
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);
                if (input.Length == 3 && input[2] == "M")
                {
                    board.MarkMine(x, y);
                }
                else
                {
                    board.CheckField(x, y);
                }

                if (board.CurrentMines == 0)
                {
                    board.CurrentGameState = Won;
                }

            }

            Console.Clear();
            board.ShowAllFields();

            if (board.CurrentGameState == Won)
            {
                Console.WriteLine("You won!");
            }
            else
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
