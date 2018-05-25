using FluentAssertions;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class SingleLineBattleshipPlacementTests
    {
        [Theory]
        [InlineData(Direction.Across)]
        [InlineData(Direction.Down)]
        public void ShouldReturnPositionsForSingleInlinePlacement(Direction direction)
        {
            var inlineBattleshipPlacement = new SingleLineBattleshipPlacement(1);
            var shipPlacement = new ShipPlacement('A', 0, direction);

            var shipPositions = inlineBattleshipPlacement.GetShipPositions(shipPlacement);

            shipPositions.Should().Equal(('A', 0));
        }

        [Fact]
        public void ShouldReturnPositionsForAcross()
        {
            var inlineBattleshipPlacement = new SingleLineBattleshipPlacement(3);
            var shipPlacement = new ShipPlacement('C', 0, Direction.Across);

            var shipPositions = inlineBattleshipPlacement.GetShipPositions(shipPlacement);

            shipPositions.Should().Equal(('C', 0), ('D', 0), ('E', 0));
        }

        [Fact]
        public void ShouldReturnPositionsForDown()
        {
            var inlineBattleshipPlacement = new SingleLineBattleshipPlacement(3);
            var shipPlacement = new ShipPlacement('D', 3, Direction.Down);

            var shipPositions = inlineBattleshipPlacement.GetShipPositions(shipPlacement);

            shipPositions.Should().Equal(('D', 3), ('D', 4), ('D', 5));
        }
    }
}