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
                board.PrintBoard();
                Console.WriteLine("Podaj współrzędne: ");
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
                    Console.WriteLine("You win!");
                    board.CurrentGameState = Won;
                }

            }
            board.ShowAllFields();

        }
    }

    public enum GameState
    {
        InGame,
        Lost,
        Won
    }
}
