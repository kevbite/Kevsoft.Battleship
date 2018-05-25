using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattleshipGameTests
    {
        private readonly Mock<IBattlefield> _battlefield;
        private readonly BattleshipGame _battleshipGame;

        public BattleshipGameTests()
        {
            _battlefield = new Mock<IBattlefield>();
            _battleshipGame = new BattleshipGame(_battlefield.Object);
        }

        [Fact]
        public void ShouldBeIncompletedGameWhenNotAllShipsShot()
        {
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {('A', 1), TestCells.OccupiedCell},
                    {('B', 1), TestCells.OccupiedCell}
                });

            _battleshipGame.IsComplete.Should().BeFalse();
        }

        [Fact]
        public void ShouldBeCompletedGameWhenShipsShot()
        {
            var position = ('A', 1);
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {position, TestCells.OccupiedCell},
                    {('B', 1), TestCells.EmptyCell}
                });

            _battleshipGame.Fire(position);

            _battleshipGame.IsComplete.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseOnFiringSameCell()
        {
            var position = ('A', 1);
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>());

            _battleshipGame.Fire(position).Should().BeTrue();
            _battleshipGame.Fire(position).Should().BeFalse();
        }
    }
}
