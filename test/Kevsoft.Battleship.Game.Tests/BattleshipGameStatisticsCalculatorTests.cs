using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattleshipGameStatisticsCalculatorTests
    {
        private readonly GameStatisticsCalculator _calculator;

        public BattleshipGameStatisticsCalculatorTests()
        {
            _calculator = new GameStatisticsCalculator();
        }

        [Fact]
        public void ShouldReturnCorrectStatistics()
        {
            var battleshipGame = new Mock<IReadOnlyBattleshipGame>();

            battleshipGame.Setup(x => x.Cells)
                .Returns(new HashSet<(char x, int y)>(new[] { ('A', 1), ('B', 1),('C', 1), ('D', 1) }));
            battleshipGame.Setup(x => x.Hits)
                .Returns(new HashSet<(char x, int y)>(new[] { ('A', 1), ('D', 1) }));
            battleshipGame.Setup(x => x.Misses)
                .Returns(new HashSet<(char x, int y)>(new[] { ('B', 1), ('C', 1) }));

            var statistics = _calculator.GetCurrentStatistics(battleshipGame.Object);

            statistics.Hits.Should().Be(2);
            statistics.Misses.Should().Be(2);
            statistics.TotalShots.Should().Be(4);
            statistics.Accuracy.Should().Be(0.5);
        }
    }
}
