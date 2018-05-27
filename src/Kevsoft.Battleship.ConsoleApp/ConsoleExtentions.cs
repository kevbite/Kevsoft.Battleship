using System;

namespace Kevsoft.Battleship.ConsoleApp
{
    public static class ConsoleExtentions
    {
        public static void WithPosition(this IConsole console, Action<IConsole> action, int left, int top)
        {
            var lastCursorLeft = console.CursorLeft;
            var lastCursorTop = console.CursorTop;

            console.SetCursorPosition(left, top);

            action(console);

            console.SetCursorPosition(lastCursorLeft, lastCursorTop);
        }

        public static void WriteAtPosition(this IConsole console, int left, int top, string value)
        {
            console.WithPosition(x => x.Write(value), left, top);
        }

        public static void WriteAtPositionWithForegroundColor(this IConsole console, int left, int top, string value, ConsoleColor color)
        {
            console.WithPosition(x => x.WriteWithForegroundColor(value, ConsoleColor.Red), left, top);
        }

        public static void WithColor(this IConsole console, Action<IConsole> action, ConsoleColor color)
        {
            var lastForeColor = console.ForegroundColor;

            console.ForegroundColor = color;

            action(console);

            console.ForegroundColor = lastForeColor;
        }

        public static void WriteWithForegroundColor(this IConsole console, char value, ConsoleColor color)
        {
            console.WithColor(x => x.Write(value), color);
        }

        public static void WriteWithForegroundColor(this IConsole console, string value, ConsoleColor color)
        {
            console.WithColor(x => x.Write(value), color);
        }
    }
}