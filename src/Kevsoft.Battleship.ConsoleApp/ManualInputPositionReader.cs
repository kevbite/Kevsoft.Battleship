using System;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class ManualInputPositionReader : IPositionReader
    {
        private readonly IConsole _console;

        public ManualInputPositionReader(IConsole console)
        {
            _console = console;
        }

        public (char x, int y) ReadPosition()
        {
            _console.Write("Fire at (C5): ");
            char x;
            int? y;
            do
            {
                x = ReadX();
                y = ReadY();
            } while (!y.HasValue);

            return (x, y.Value);
        }

        private int? ReadY()
        {
            var informationText = "Enter a digit value (0-9)";
            var input = string.Empty;
            ConsoleKeyInfo key;
            do
            {
                key = _console.ReadKey(true);
                var c = key.KeyChar;
                _console.WriteAtPosition(0, _console.CursorTop + 1, new string(' ', informationText.Length));
                if (key.Key == ConsoleKey.Backspace)
                {
                    _console.Write("\b \b");
                    if (input.Length == 0)
                    {
                        return null;
                    }
                    input = input.Substring(0, input.Length - 1);
                } else if (char.IsDigit(c))
                {
                    if (input.Length < 2)
                    {
                        input += c;
                        _console.Write(c);
                    }
                }
                else
                {
                    _console.WriteAtPositionWithForegroundColor(0, _console.CursorTop + 1, informationText, ConsoleColor.Red);
                }

            } while (key.Key != ConsoleKey.Enter || string.IsNullOrEmpty(input));

            return int.Parse(input);
        }

        private char ReadX()
        {
            var informationText = "Enter a letter (A-Z)";
            char x;
            do
            {
                x = _console.ReadKey(true).KeyChar;
                _console.WriteAtPosition(0, _console.CursorTop + 1, new string(' ', informationText.Length));
                if (!char.IsLetter(x))
                {
                    _console.WriteAtPositionWithForegroundColor(0, _console.CursorTop + 1, informationText, ConsoleColor.Red);
                }
            } while (!char.IsLetter(x));

            x = char.ToUpperInvariant(x);
            _console.Write(x);
            return x;
        }
    }
}