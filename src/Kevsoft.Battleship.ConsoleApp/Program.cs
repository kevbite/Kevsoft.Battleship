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

            var battleshipGame = new StandardBattleshipGameBuilder()
                .WithShip(ShipType.Battleship)
                .WithShip(ShipType.Destroyer)
                .WithShip(ShipType.Destroyer)
                .Build();

            var consoleWrapper = new ConsoleWrapper();
            var gameDrawer = new GameDrawer(consoleWrapper);
            var arrowInputPositionReader = new ArrowInputPositionReader(gameDrawer, battleshipGame, consoleWrapper);
            var manualInputPositionReader = new ManualInputPositionReader(consoleWrapper);
            var consolePlayer = new ConsolePlayer(consoleWrapper, gameDrawer, arrowInputPositionReader);

            consolePlayer.Play(battleshipGame);


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}
