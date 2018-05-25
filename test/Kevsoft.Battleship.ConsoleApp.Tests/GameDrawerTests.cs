using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Kevsoft.Battleship.Game;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.ConsoleApp.Tests
{
    public class GameDrawerTests
    {
        private readonly GameDrawer _gameDrawer;
        private readonly StringWriter _writer;

        public GameDrawerTests()
        {
            _writer = new StringWriter();
            _gameDrawer = new GameDrawer(_writer);
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
    }
}
