using System.Linq;
using FluentAssertions;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattlefieldTests
    {
        [Fact]
        public void ShouldCreateBattlefieldWithNoShipsPlaced()
        {
            var battlefield = new Battlefield(5);

            battlefield.Cells.Where(x => !x.Value.HasShipPlaced)
                .Select(x => x.Key)
                .Should().BeEquivalentTo(
                    ('A', 1), ('B', 1), ('C', 1), ('D', 1), ('E', 1),
                    ('A', 2), ('B', 2), ('C', 2), ('D', 2), ('E', 2),
                    ('A', 3), ('B', 3), ('C', 3), ('D', 3), ('E', 3),
                    ('A', 4), ('B', 4), ('C', 4), ('D', 4), ('E', 4),
                    ('A', 5), ('B', 5), ('C', 5), ('D', 5), ('E', 5)
                );
        }
    }
}