using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Kevsoft.Battleship.Game.Tests
{
    public class StandardBattleshipGameBuilderTests
    {
        public void ShouldBuildGameWithCorrectAmountOfShips()
        {
            var battleshipGame = new StandardBattleshipGameBuilder()
                .WithShip(ShipType.Battleship)
                .WithShip(ShipType.Destroyer)
                .WithShip(ShipType.Destroyer)
                .Build();

            battleshipGame.FireAllCells();

            battleshipGame.Cells.Count.Should().Be(10 * 10);
            battleshipGame.Hits.Count.Should().Be(13);
        }
    }
}
