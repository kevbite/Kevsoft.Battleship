using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class RandomPositionTests
    {
        private readonly Fixture _fixture;
        private readonly RandomPosition _randomPosition;
        private readonly Mock<IRandom> _random;

        public RandomPositionTests()
        {
            _fixture = new Fixture();
            _random = new Mock<IRandom>();
            _randomPosition = new RandomPosition(_random.Object);
        }

        [Fact]
        public void ShouldPickRandomNextValue()
        {
            var values = _fixture.CreateMany<(char, int)>().ToArray();

            _random.Setup(x => x.Next(0, values.Length))
                .Returns(2);

            _randomPosition.Next(values)
                .Should().Be(values[2]);
        }
    }
}