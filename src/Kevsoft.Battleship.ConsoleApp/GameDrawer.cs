using System;
using System.IO;
using System.Linq;
using Kevsoft.Battleship.Game;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class GameDrawer
    {
        private readonly TextWriter _out;
        private readonly Action<ConsoleColor> _setColor;
        private readonly Action _resetColor;

        public GameDrawer(TextWriter @out, Action<ConsoleColor> setColor, Action resetColor)
        {
            _out = @out;
            _setColor = setColor;
            _resetColor = resetColor;
        }

        public void Draw(IReadOnlyBattleshipGame game)
        {
            var minX = game.Cells.Min(pos => pos.x);
            var maxX = game.Cells.Max(pos => pos.x);
            var minY = game.Cells.Min(pos => pos.y);
            var maxY = game.Cells.Max(pos => pos.y);

            DrawHeader(minX, maxX);
            for (var y = minY; y <= maxY; y++)
            {
                _out.Write($" {y,-2}");
                for (var x = minX; x <= maxX; x++)
                {
                    _out.Write($"| ");
                    WriteMove((x, y), game);
                    _out.Write(" ");
                }
                _out.WriteLine();

                DrawSeparator(minX, maxX);
            }
        }

        private void WriteMove((char, int) pos, IReadOnlyBattleshipGame game)
        {
            var value = ' ';
            
            if (game.Hits.Contains(pos))
            {
                _setColor(ConsoleColor.Red);
                value ='*';
            }
            else if (game.Misses.Contains(pos))
            {
                _setColor(ConsoleColor.Green);
                value ='X';
            }

            _out.Write(value);
            Console.ResetColor();
        }

        private void DrawSeparator(char min, char max)
        {
            _out.Write(" - ");
            for (var c = min; c <= max; c++)
            {
                _out.Write("+ - ");
            }
            _out.WriteLine();
        }

        private void DrawHeader(char min, char max)
        {
            _out.Write("   ");
            for (var c = min; c <= max; c++)
            {
                _out.Write($"| {c} ");
            }
            _out.WriteLine();

            _out.Write($"---");
            for (var c = min; c <= max; c++)
            {
                _out.Write($"+---");
            }
            _out.WriteLine();
        }
    }
}