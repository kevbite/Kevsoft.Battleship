using System.Linq;
using AutoFixture;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattleshipTestsForPlace
    {
        private readonly Mock<IBattlefield> _battlefield;
        private readonly Mock<IBattleshipPlacement> _battleshipPlacement;
        private readonly Battleship _battleship;
        private readonly Fixture _fixture;

        public BattleshipTestsForPlace()
        {
            _fixture = new Fixture();
            _battlefield = new Mock<IBattlefield>();

            _battleshipPlacement = new Mock<IBattleshipPlacement>();
            _battleship = new Battleship(_battleshipPlacement.Object);
        }

        [Fact]
        public void ShouldMarkCellsWithShip()
        {
            var shipPlacement = _fixture.Create<ShipPlacement>();

            var shipPositions = _fixture.CreateMany<(char, int)>().ToList();
            _battleshipPlacement.Setup(x => x.GetShipPositions(shipPlacement))
                .Returns(shipPositions);

            _battleship.Place(_battlefield.Object, shipPlacement);

            foreach (var positions in shipPositions)
            {
                _battlefield.Verify(x => x.MarkCell(positions), Times.Once);
            }

            _battlefield.Verify(x => x.MarkCell(It.IsAny<(char, int)>()), Times.Exactly(shipPositions.Count));
        }
    }
}