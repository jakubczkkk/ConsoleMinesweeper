using System;
using static ConsoleMinesweeper.GameState;

namespace ConsoleMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {

            var board = new Board();
            GameState.IsPlaying = true;
            while (GameState.IsPlaying)
            {
                board.PrintBoard();
                Console.WriteLine("Podaj współrzędne: ");
                string[] input = Console.ReadLine().Split();
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);
                board.CheckField(x, y);
            }
            board.ShowAllFields();

        }
    }
}
