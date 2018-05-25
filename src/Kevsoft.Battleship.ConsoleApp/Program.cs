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

            var battleshipGame = new BattleshipGame(battlefield);
            
            while (!battleshipGame.IsComplete)
            {
                Console.Clear();
                Console.Write("Fire at (eg C2): ");
                var x = Console.ReadKey().KeyChar;
                var y = (int)char.GetNumericValue(Console.ReadKey().KeyChar);

                if (!battleshipGame.Fire((x, y)))
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid move, press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}
