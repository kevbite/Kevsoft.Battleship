using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class PositionOnBattlefieldValidatorTests
    {
        private readonly PositionOnBattlefieldValidator _validator;

        public PositionOnBattlefieldValidatorTests()
        {
            _validator = new PositionOnBattlefieldValidator();
        }

        [Fact]
        public void ShouldReturnTrueIfOnGrid()
        {
            var battlefield = new Mock<IBattlefield>();

            battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {('A', 1), null},
                    {('B', 1), null}
                });
            var result = _validator.Validate(('A', 1), battlefield.Object);

            result.Should().BeTrue();
        }


        [Fact]
        public void ShouldReturnFalseIfOffGrid()
        {
            var battlefield = new Mock<IBattlefield>();

            battlefield.Setup(x => x.Cells)
                .Returns(new Dictionary<(char x, int y), IBattlefieldCell>
                {
                    {('A', 1), null},
                    {('B', 1), null}
                });

            var result = _validator.Validate(('Z', 99), battlefield.Object);

            result.Should().BeFalse();
        }
    }
}
