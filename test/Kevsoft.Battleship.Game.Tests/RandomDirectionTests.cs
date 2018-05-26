using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class RandomDirectionTests
    {
        private readonly Mock<IRandom> _random;
        private readonly RandomDirection _randomDirection;

        public RandomDirectionTests()
        {
            _random = new Mock<IRandom>();
            _randomDirection = new RandomDirection(_random.Object);
        }

        [Theory]
        [InlineData(0, Direction.Across)]
        [InlineData(1, Direction.Down)]
        public void  ShouldReturnCorrectValues(int value, Direction direction)
        {
            _random.Setup(x => x.Next(0, 2))
                .Returns(value);

            _randomDirection.Next()
                .Should().Be(direction);
        }
    }
}