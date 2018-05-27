using System;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class  ConsoleWrapper : IConsole
    {
        public void Write(string format)
        {
            Console.Write(format);
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public int CursorLeft => Console.CursorLeft;

        public int CursorTop => Console.CursorTop;

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}