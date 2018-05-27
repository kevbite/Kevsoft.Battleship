using System;
using System.IO;
using System.Linq;
using Kevsoft.Battleship.Game;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class GameDrawer
    {
        private readonly IConsole _console;

        public GameDrawer(IConsole console)
        {
            _console = console;
        }

        public void Draw(IReadOnlyBattleshipGame game, (char, int)? cursor = null)
        {
            var minX = game.Cells.Min(pos => pos.x);
            var maxX = game.Cells.Max(pos => pos.x);
            var minY = game.Cells.Min(pos => pos.y);
            var maxY = game.Cells.Max(pos => pos.y);

            DrawHeader(minX, maxX);
            for (var y = minY; y <= maxY; y++)
            {
                _console.Write($" {y,-2}");
                for (var x = minX; x <= maxX; x++)
                {
                    _console.Write("| ");
                    WriteMove((x, y), game, cursor);
                    _console.Write(" ");
                }
                _console.WriteLine();

                DrawSeparator(minX, maxX);
            }
        }

        private void WriteMove((char x, int y) pos, IReadOnlyBattleshipGame game, (char x, int y)? cursor)
        {
            var value = ' ';
            var foregroundColor = _console.ForegroundColor;
            if (cursor.HasValue && cursor.Value.x == pos.x && cursor.Value.y == pos.y)
            {
                foregroundColor = ConsoleColor.Blue;
                value = '+';
            }
            if (game.Hits.Contains(pos))
            {
                foregroundColor = ConsoleColor.Red;
                value = '*';
            }
            else if (game.Misses.Contains(pos))
            {
                foregroundColor = ConsoleColor.White;
                value = 'X';
            }

            _console.WriteWithForegroundColor(value, foregroundColor);
        }

        private void DrawSeparator(char min, char max)
        {
            _console.Write(" - ");
            for (var c = min; c <= max; c++)
            {
                _console.Write("+ - ");
            }
            _console.WriteLine();
        }

        private void DrawHeader(char min, char max)
        {
            _console.Write("   ");
            for (var c = min; c <= max; c++)
            {
                _console.Write($"| {c} ");
            }
            _console.WriteLine();

            _console.Write("---");
            for (var c = min; c <= max; c++)
            {
                _console.Write($"+---");
            }
            _console.WriteLine();
        }
    }
}