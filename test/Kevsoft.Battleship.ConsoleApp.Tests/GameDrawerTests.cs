using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Kevsoft.Battleship.Game;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.ConsoleApp.Tests
{
    public class TextWriterConsole : IConsole
    {
        private readonly TextWriter _textWriter;

        public TextWriterConsole(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void Write(string value)
        {
            _textWriter.Write(value);
        }

        public void Write(char value)
        {
            _textWriter.Write(value);
        }

        public void WriteLine()
        {
            _textWriter.WriteLine();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            throw new NotImplementedException();
        }

        public int CursorLeft { get; }

        public int CursorTop { get; }

        public ConsoleColor ForegroundColor { get; set; }

        public int WindowWidth { get; }

        public void SetCursorPosition(int left, int top)
        {
            throw new NotImplementedException();
        }

    }
    public class GameDrawerTests
    {
        private readonly GameDrawer _gameDrawer;
        private readonly StringWriter _writer;

        public GameDrawerTests()
        {
            _writer = new StringWriter();
            _gameDrawer = new GameDrawer(new TextWriterConsole(_writer));
        }

        [Fact]
        public void DrawsGame()
        {
            var game = new Mock<IReadOnlyBattleshipGame>();
            game.Setup(x => x.Cells)
                .Returns(new HashSet<(char x, int y)>
                {
                    ('A', 1), ('B', 1), ('C', 1),
                    ('A', 2), ('B', 2), ('C', 2),
                    ('A', 3), ('B', 3), ('C', 3),
                });
            game.Setup(x => x.Hits)
                .Returns(new HashSet<(char x, int y)>
                {
                    ('A', 1)
                });
            game.Setup(x => x.Misses)
                .Returns(new HashSet<(char x, int y)>
                {
                    ('A', 3)
                });

            _gameDrawer.Draw(game.Object);

            _writer.ToString()
                .Should().Be(
                    "   | A | B | C " + Environment.NewLine +
                    "---+---+---+---" + Environment.NewLine +
                    " 1 | * |   |   " + Environment.NewLine +
                    " - + - + - + - " + Environment.NewLine +
                    " 2 |   |   |   " + Environment.NewLine +
                    " - + - + - + - " + Environment.NewLine +
                    " 3 | X |   |   " + Environment.NewLine +
                    " - + - + - + - " + Environment.NewLine);
        }

        [Fact]
        public void DrawsDoubleFigures()
        {
            var game = new Mock<IReadOnlyBattleshipGame>();
            game.Setup(x => x.Cells)
                .Returns(new HashSet<(char x, int y)>
                {
                    ('A', 10)
                });
            game.Setup(x => x.Hits).Returns(new HashSet<(char x, int y)>());
            game.Setup(x => x.Misses).Returns(new HashSet<(char x, int y)>());


            _gameDrawer.Draw(game.Object);

            _writer.ToString()
                .Should().Be(
                    "   | A " + Environment.NewLine +
                    "---+---" + Environment.NewLine +
                    " 10|   " + Environment.NewLine +
                    " - + - " + Environment.NewLine);
        }
    }
}
