using System;
using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class Shipyard
    {
        private readonly IDictionary<ShipType, Func<IShip>> _shipFactories;

        public Shipyard(IDictionary<ShipType, Func<IShip>> shipFactories)
        {
            _shipFactories = shipFactories;
        }
        public IShip CreateShip(ShipType type)
        {
            return _shipFactories[type]();
        }
    }
}