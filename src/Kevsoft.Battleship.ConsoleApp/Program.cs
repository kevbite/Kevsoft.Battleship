using System;
using Kevsoft.Battleship.Game;

namespace Kevsoft.Battleship.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Battleships!");
            Console.WriteLine("=======================");
            Console.WriteLine();
            Console.WriteLine("Press any key to get started...");
            Console.ReadKey();
            var battlefield = new Battlefield(10);
            var shipPlacer = new ShipPlacer(battlefield);
            shipPlacer.AddShip(new Game.Battleship(new SingleLineBattleshipPlacement(2)), new ShipPlacement('A', 1, Direction.Across));
            shipPlacer.AddShip(new Game.Battleship(new SingleLineBattleshipPlacement(1)), new ShipPlacement('B', 2, Direction.Down));

            var battleshipGame = new BattleshipGame(battlefield, new PositionOnBattlefieldValidator(), new GameStatisticsCalculator());
            var gameDrawer = new GameDrawer(Console.Out, c => Console.ForegroundColor = c, Console.ResetColor);
            while (!battleshipGame.IsComplete)
            {
                Console.Clear();
                gameDrawer.Draw(battleshipGame);
                Console.WriteLine();
                Console.Write("Fire at: ");
                var x = ReadX();
                var y = ReadY();

                if (!battleshipGame.Fire((x, y)).ShotFired)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid move, press any key to continue...");
                    Console.ReadKey();
                }
            }

            Console.Clear();
            gameDrawer.Draw(battleshipGame);
            Console.WriteLine();
            Console.WriteLine("Game Completed!");
            var statistics = battleshipGame.CurrentStatistics;

            Console.WriteLine($"Hits: {statistics.Hits}");
            Console.WriteLine($"Misses: {statistics.Misses}");
            Console.WriteLine($"Accuracy: {statistics.Accuracy:P}");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static int ReadY()
        {
            var input = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                var c = key.KeyChar;
                if (char.IsDigit(c))
                {
                    input += c;
                    Console.Write(c);
                }
            } while (key.Key != ConsoleKey.Enter || string.IsNullOrEmpty(input));

            return int.Parse(input);
        }

        private static char ReadX()
        {
            char x;
            do
            {
                x = Console.ReadKey(true).KeyChar;
            } while (!char.IsLetter(x));

            x = char.ToUpperInvariant(x);
            Console.Write(x);
            return x;
        }
    }
}
