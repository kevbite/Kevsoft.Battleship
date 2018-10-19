using System;
using Kevsoft.Battleship.Game;

namespace Kevsoft.Battleship.ConsoleApp
{
    public class ArrowInputPositionReader : IPositionReader
    {
        private readonly GameDrawer _gameDrawer;
        private readonly BattleshipGame _game;
        private readonly IConsole _console;
        private (char x, int y) _readPosition;

        public ArrowInputPositionReader(GameDrawer gameDrawer, BattleshipGame game, IConsole console)
        {
            _gameDrawer = gameDrawer;
            _game = game;
            _console = console;
            _readPosition = (x: 'A', y: 1);
        }

        public (char x, int y) ReadPosition()
        {
            ConsoleKeyInfo key;
            DrawCursor(_readPosition);
            do
            {
                key = _console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    _readPosition = (_readPosition.x, --_readPosition.y);
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    _readPosition = (_readPosition.x, ++_readPosition.y);
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _readPosition = (--_readPosition.x, _readPosition.y);
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    _readPosition = (++_readPosition.x, _readPosition.y);
                }
                _console.Clear();
                _gameDrawer.Draw(_game, _readPosition);
            } while (key.Key != ConsoleKey.Enter);

            return _readPosition;
        }

        private void ClearCursor((char x, int y) pos)
        {
            var cursor = GetCursor(pos);
            _console.WriteAtPosition(cursor.left, cursor.top, " ");
        }

        private void DrawCursor((char x, int y) pos)
        {
            var cursor = GetCursor(pos);
            _console.WriteAtPositionWithForegroundColor(cursor.left, cursor.top, "+", ConsoleColor.Blue);
        }

        private static (int left, int top) GetCursor((char x, int y) pos)
        {
            var left = (pos.x - 'A' + 1) * 4 + 1;
            var top = pos.y * 2;

            return (left, top);
        }
    }
}