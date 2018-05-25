using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattleshipTestsForCanPlace
    {
        private readonly Mock<IBattlefield> _battlefield;
        private readonly Mock<IBattleshipPlacement> _battleshipPlacement;
        private readonly Battleship _battleship;
        private readonly Fixture _fixture;

        public BattleshipTestsForCanPlace()
        {
            _fixture = new Fixture();
            _battlefield = new Mock<IBattlefield>();

            _battleshipPlacement = new Mock<IBattleshipPlacement>();
            _battleship = new Battleship(_battleshipPlacement.Object);
        }

        [Fact]
        public void ShouldNotBeAbleToPlaceWhenNoCellsAreAvailable()
        {
            var shipPlacement = _fixture.Create<ShipPlacement>();
            _battleshipPlacement.Setup(x => x.GetShipPositions(shipPlacement))
                .Returns(new[] { ('A', 1) });

            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>());

            var canPlace = _battleship.CanPlace(_battlefield.Object, shipPlacement);

            canPlace.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeAbleToPlaceABattleShipOnToASingleEmptyCelledBattlefield()
        {
            var shipPlacement = _fixture.Create<ShipPlacement>();
            _battleshipPlacement.Setup(x => x.GetShipPositions(shipPlacement))
                .Returns(new[] { ('A', 1) });

            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>()
                {
                    {('A', 1), TestCells.EmptyCell }
                });

            var canPlace = _battleship.CanPlace(_battlefield.Object, shipPlacement);

            canPlace.Should().BeTrue();
        }

        [Fact]
        public void ShouldBeAbleToPlaceABattleShipOnToMultipleEmptyCelledBattlefield()
        {
            var shipPlacement = _fixture.Create<ShipPlacement>();
            _battleshipPlacement.Setup(x => x.GetShipPositions(shipPlacement))
                .Returns(new[] { ('A', 1), ('A', 2), ('A', 3) });

            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>()
                {
                    {('A', 1), TestCells.EmptyCell },
                    {('A', 2), TestCells.EmptyCell },
                    {('A', 3), TestCells.EmptyCell },
                });

            var canPlace = _battleship.CanPlace(_battlefield.Object, shipPlacement);

            canPlace.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotBeAbleToPlaceABattleShipOnToMultipleCelledWithOneOccupiedCellBattlefield()
        {
            var shipPlacement = _fixture.Create<ShipPlacement>();
            _battleshipPlacement.Setup(x => x.GetShipPositions(shipPlacement))
                .Returns(new[] { ('A', 1), ('A', 2), ('A', 3) });

            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>()
                {
                    {('A', 1), TestCells.EmptyCell },
                    {('A', 2), TestCells.OccupiedCell },
                    {('A', 3), TestCells.EmptyCell },
                });

            var canPlace = _battleship.CanPlace(_battlefield.Object, shipPlacement);

            canPlace.Should().BeFalse();
        }
    }
}