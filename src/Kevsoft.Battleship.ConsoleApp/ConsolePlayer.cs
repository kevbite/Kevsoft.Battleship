using System;
using Kevsoft.Battleship.Game;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class ConsolePlayer
    {
        private readonly IConsole _console;
        private readonly GameDrawer _gameDrawer;
        private readonly IPositionReader _positionReader;

        public ConsolePlayer(IConsole console, GameDrawer gameDrawer, IPositionReader positionReader)
        {
            _console = console;
            _gameDrawer = gameDrawer;
            _positionReader = positionReader;
        }

        public void Play(BattleshipGame battleshipGame)
        {
            var position = new (char x, int y)?();
            while (!battleshipGame.IsComplete)
            {
                _console.Clear();
                _gameDrawer.Draw(battleshipGame, position);
                _console.WriteLine();
                position = _positionReader.ReadPosition();

                if (!battleshipGame.Fire(position.Value).ShotFired)
                {
                    _console.WriteLine();
                    _console.Write("Invalid move, press any key to continue...");
                    _console.WriteLine();
                    _console.ReadKey(true);
                }
            }

            _console.Clear();
            _gameDrawer.Draw(battleshipGame);
            _console.WriteLine();
            _console.WriteLine("Game Completed!");
            var statistics = battleshipGame.CurrentStatistics;

            _console.WriteLine($"Hits: {statistics.Hits}");
            _console.WriteLine($"Misses: {statistics.Misses}");
            _console.WriteLine($"Accuracy: {statistics.Accuracy:P}");

            _console.WriteLine();
        }

   
    }
}