using System;
using System.Collections.Generic;
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
            var lastMessage = string.Empty;
            while (!battleshipGame.IsComplete)
            {
                _console.Clear();
                _gameDrawer.Draw(battleshipGame, position);
                _console.WriteLine();
                _console.WriteAtPositionWithForegroundColor(0, _console.CursorTop +1, lastMessage, ConsoleColor.Yellow);
                position = _positionReader.ReadPosition();

                var fireResult = battleshipGame.Fire(position.Value);
                lastMessage = _fireResultMessages[fireResult];
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

        private readonly IReadOnlyDictionary<FireResult, string> _fireResultMessages = new Dictionary<FireResult, string>
        {
            {FireResult.Invalid, "Invalid move!" },
            {FireResult.AlreadyFired, "You've shot this before!" },
            {FireResult.Missed, "Damn you missed!" },
            {FireResult.Hit, "Nice one, you got a hit!" },
        };
    }
}