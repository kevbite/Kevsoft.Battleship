using Moq;

namespace Kevsoft.Battleship.Game.Tests
{
    public static class TestCells
    {
        public static IBattlefieldCell EmptyCell { get; }

        public static IBattlefieldCell OccupiedCell { get; }

        static TestCells()
        {
            var emptyCell = new Mock<IBattlefieldCell>();
            emptyCell.Setup(x => x.HasShipPlaced)
                .Returns(false);

            EmptyCell = emptyCell.Object;

            var occupiedCell = new Mock<IBattlefieldCell>();
            occupiedCell.Setup(x => x.HasShipPlaced)
                .Returns(true);

            OccupiedCell = occupiedCell.Object;
        }
    }
}