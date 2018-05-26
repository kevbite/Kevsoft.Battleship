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
        private readonly Mock<IShotValidator> _shotValidator;
        private readonly Mock<IGameStatisticsCalculator> _statisticsCalculator;

        public BattleshipGameTests()
        {
            _fixture = new Fixture();
            _battlefield = new Mock<IBattlefield>();
            _shotValidator = new Mock<IShotValidator>();
            _statisticsCalculator = new Mock<IGameStatisticsCalculator>();

            _battleshipGame = new BattleshipGame(_battlefield.Object, _shotValidator.Object, _statisticsCalculator.Object);
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

            _shotValidator.Setup(x => x.Validate(position, _battlefield.Object))
                .Returns(true);

            _battleshipGame.Fire(position);

            _battleshipGame.IsComplete.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnFalseOnFiringSameCell()
        {
            var position = ('A', 1);
            _battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>());
            _shotValidator.Setup(x => x.Validate(position, _battlefield.Object))
                .Returns(true);

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

            _shotValidator.Setup(x => x.Validate(It.IsIn(position1, position2), _battlefield.Object))
                .Returns(true);

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
            _shotValidator.Setup(x => x.Validate(It.IsIn(position1, position2), _battlefield.Object))
                .Returns(true);

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

        [Fact]
        public void ShouldNotFireShotForInvalidFire()
        {
            var position1 = _fixture.Create<(char, int)>();

            _shotValidator.Setup(x => x.Validate(position1, _battlefield.Object))
                .Returns(false);

            _battleshipGame.Fire(position1).ShotFired.Should().BeFalse();

        }

        public void ShouldReturnCalculatedStatistics()
        {
            var gameStatistics = _fixture.Create<GameStatistics>();
            _statisticsCalculator.Setup(x => x.GetCurrentStatistics(_battleshipGame))
                .Returns(gameStatistics);

            _battleshipGame.CurrentStatistics.Should().BeSameAs(gameStatistics);
        }
    }

    
}
