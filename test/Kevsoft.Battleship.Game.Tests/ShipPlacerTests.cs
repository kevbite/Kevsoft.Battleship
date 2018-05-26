using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class ShipPlacerTests
    {
        private readonly Fixture _fixture;
        private readonly ShipPlacer _shipPlacer;
        private readonly IBattlefield _battlefield;

        public ShipPlacerTests()
        {
            _fixture = new Fixture();
            _battlefield = Mock.Of<IBattlefield>();
            _shipPlacer = new ShipPlacer();
        }

        [Fact]
        public void ShouldPlaceShipIfPossible()
        {
            var ship = new Mock<IShip>();
            var placement = _fixture.Create<ShipPlacement>();

            ship.Setup(x => x.CanPlace(_battlefield, placement))
                .Returns(false);

            _shipPlacer.AddShip(_battlefield, ship.Object, placement)
                .Should().BeFalse();

            ship.Verify(x => x.Place(It.IsAny<IBattlefield>(), It.IsAny<ShipPlacement>()), Times.Never);
        }
    
        [Fact]
        public void ShouldNotPlaceShipIfNotPossible()
        {
            var ship = new Mock<IShip>();
            var placement = _fixture.Create<ShipPlacement>();

            ship.Setup(x => x.CanPlace(_battlefield, placement))
                .Returns(true);

            _shipPlacer.AddShip(_battlefield, ship.Object, placement)
                .Should().BeTrue();

            ship.Verify(x => x.Place(_battlefield, placement), Times.Once);
        }
    }
}
