using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class RandomShipPlacerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRandomDirection> _randomDirection;
        private readonly Mock<IRandomPosition> _randomPosition;
        private readonly RandomShipPlacer _placer;
        private readonly int _maxAttempts;
        private Mock<IBattlefield> _battlefield;

        public RandomShipPlacerTests()
        {
            _fixture = new Fixture();
            _randomDirection = new Mock<IRandomDirection>();
            _randomPosition = new Mock<IRandomPosition>();

            _maxAttempts = 10;
            _placer = new RandomShipPlacer(_randomDirection.Object, _randomPosition.Object, _maxAttempts);


            var positions = new[] { ('A', 1), ('B', 1), ('A', 2), ('B', 2) };
            var cells = new Mock<IReadOnlyDictionary<(char, int), IBattlefieldCell>>();
            cells.Setup(x => x.Keys).Returns(positions);

            _battlefield = new Mock<IBattlefield>();
            _battlefield.Setup(x => x.Cells)
                .Returns(cells.Object);
        }

        [Fact]
        public void ShouldPlaceShipInRandomPlaces()
        {
            var direction = _fixture.Create<Direction>();
            _randomDirection.Setup(x => x.Next())
                .Returns(direction);

            _randomPosition.Setup(x => x.Next(_battlefield.Object.Cells.Keys))
                .Returns(('B', 1));

            var ship = new Mock<IShip>();

            ship.Setup(x => x.CanPlace(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                    shipPlacement.X == 'B' && shipPlacement.Y == 1 && shipPlacement.Direction == direction)))
                .Returns(true);

            _placer.AddShip(_battlefield.Object, ship.Object)
                .Should().BeTrue();

            ship.Verify(x => x.Place(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                shipPlacement.X == 'B' && shipPlacement.Y == 1 && shipPlacement.Direction == direction)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFalseIfCantPlaceShips()
        {
            var direction = _fixture.Create<Direction>();
            _randomDirection.Setup(x => x.Next())
                .Returns(direction);

            _randomPosition.Setup(x => x.Next(_battlefield.Object.Cells.Keys))
                .Returns(_fixture.Create<(char, int)>());

            var ship = new Mock<IShip>();

            ship.Setup(x => x.CanPlace(_battlefield.Object, It.IsAny<ShipPlacement>()))
                .Returns(false);

            _placer.AddShip(_battlefield.Object, ship.Object)
                .Should().BeFalse();

            ship.Verify(x => x.Place(It.IsAny<Battlefield>(), It.IsAny<ShipPlacement>()), Times.Never);
        }

        [Fact]
        public void ShouldStillPlaceShipAfterFailedAttempt()
        {
            var direction = _fixture.Create<Direction>();
            _randomDirection.Setup(x => x.Next())
                .Returns(direction);

            var attempt1 = (x: 'B', y: 1);
            var attempt2 = (x: 'A', y: 1);
            _randomPosition.SetupSequence(x => x.Next(_battlefield.Object.Cells.Keys))
                .Returns(attempt1)
                .Returns(attempt2);

            var ship = new Mock<IShip>();

            ship.Setup(x => x.CanPlace(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                    shipPlacement.X == attempt1.x && shipPlacement.Y == attempt1.y && shipPlacement.Direction == direction)))
                .Returns(false);
            ship.Setup(x => x.CanPlace(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                    shipPlacement.X == attempt2.x && shipPlacement.Y == attempt2.y && shipPlacement.Direction == direction)))
                .Returns(true);

            _placer.AddShip(_battlefield.Object, ship.Object)
                .Should().BeTrue();

            ship.Verify(x => x.Place(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                shipPlacement.X == attempt1.x && shipPlacement.Y == attempt1.y && shipPlacement.Direction == direction)), Times.Never);

            ship.Verify(x => x.Place(_battlefield.Object, It.Is<ShipPlacement>(shipPlacement =>
                shipPlacement.X == attempt2.x && shipPlacement.Y == attempt2.y && shipPlacement.Direction == direction)), Times.Once);
        }
    }
}
