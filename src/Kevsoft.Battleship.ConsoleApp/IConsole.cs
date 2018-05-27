using System;

namespace Kevsoft.Battleship.ConsoleApp
{
    public interface IConsole
    {
        void Write(string value);

        void Write(char value);

        ConsoleKeyInfo ReadKey(bool intercept);

        int CursorLeft { get; }

        int CursorTop { get; }

        ConsoleColor ForegroundColor { get; set; }
        int WindowWidth { get; }

        void SetCursorPosition(int left, int top);

        void WriteLine();

        void Clear();

        void WriteLine(string value);
    }
}