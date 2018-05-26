using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Kevsoft.Battleship.Game.Tests
{
    public class ShipyardTests
    {
        private readonly IShip _battleship;
        private readonly IShip _destroyer;
        private readonly Shipyard _shipyard;

        public ShipyardTests()
        {
            _battleship = Mock.Of<IShip>();
            _destroyer = Mock.Of<IShip>();

            _shipyard = new Shipyard(new Dictionary<ShipType, Func<IShip>>
            {
                {ShipType.Battleship, () => _battleship},
                {ShipType.Destroyer, () => _destroyer},
            });
        }
        [Fact]
        public void ShouldBeAbleToCreateDestroyer()
        {
            var ship = _shipyard.CreateShip(ShipType.Destroyer);

            ship.Should().BeSameAs(_destroyer);
        }


        [Fact]
        public void ShouldBeAbleToCreateBattleship()
        {
            var ship = _shipyard.CreateShip(ShipType.Battleship);

            ship.Should().BeSameAs(_battleship);
        }
    }
}
