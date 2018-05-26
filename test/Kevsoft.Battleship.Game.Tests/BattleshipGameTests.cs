using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class BattleshipGameTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IBattlefield> _battlefield;
        private readonly BattleshipGame _battleshipGame;

        public BattleshipGameTests()
        {
            _fixture = new Fixture();
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

            _battleshipGame.Fire(position).ShotFired.Should().BeTrue();
            _battleshipGame.Fire(position).ShotFired.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnHits()
        {
            var position1 = _fixture.Create<(char, int)>();
            var position2 = _fixture.Create<(char, int)>();
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {position1, TestCells.OccupiedCell},
                    {position2, TestCells.EmptyCell}
                });

            _battleshipGame.Fire(position1).ShotFired.Should().BeTrue();
            _battleshipGame.Fire(position2).ShotFired.Should().BeTrue();

            _battleshipGame.Hits.Should().Contain(position1);
            _battleshipGame.Hits.Should().NotContain(position2);
        }

        [Fact]
        public void ShouldReturnMisses()
        {
            var position1 = _fixture.Create<(char, int)>();
            var position2 = _fixture.Create<(char, int)>();
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {position1, TestCells.OccupiedCell},
                    {position2, TestCells.EmptyCell}
                });

            _battleshipGame.Fire(position1).ShotFired.Should().BeTrue();
            _battleshipGame.Fire(position2).ShotFired.Should().BeTrue();

            _battleshipGame.Misses.Should().NotContain(position1);
            _battleshipGame.Misses.Should().Contain(position2);
        }

        [Fact]
        public void ShouldReturnBoardCells()
        {
            var position1 = _fixture.Create<(char, int)>();
            var position2 = _fixture.Create<(char, int)>();

            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {position1, null},
                    {position2, null}
                });

            _battleshipGame.Cells.Should().BeEquivalentTo(position1, position2);
        }
    }
}
