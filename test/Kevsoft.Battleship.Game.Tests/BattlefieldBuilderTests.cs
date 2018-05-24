using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattlefieldBuilderTests
    {
        private readonly BattlefieldBuilder _battlefieldBuilder;
        private readonly IBattlefield _battlefield;

        public BattlefieldBuilderTests()
        {
            _battlefield = Mock.Of<IBattlefield>();
            _battlefieldBuilder = new BattlefieldBuilder(_battlefield);
        }

        [Fact]
        public void ShouldPlaceShipIfPossible()
        {
            var ship = new Mock<IShip>();
            ship.Setup(x => x.CanPlace(_battlefield))
                .Returns(false);

            _battlefieldBuilder.AddShip(ship.Object)
                .Should().BeFalse();

            ship.Verify(x => x.Place(It.IsAny<IBattlefield>()), Times.Never);
        }
    
        [Fact]
        public void ShouldNotPlaceShipIfNotPossible()
        {
            var ship = new Mock<IShip>();
            ship.Setup(x => x.CanPlace(_battlefield))
                .Returns(true);

            _battlefieldBuilder.AddShip(ship.Object)
                .Should().BeFalse();

            ship.Verify(x => x.Place(_battlefield), Times.Once);
        }
    }
}
