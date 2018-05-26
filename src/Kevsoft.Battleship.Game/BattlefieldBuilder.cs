using System;
using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class BattlefieldBuilder
    {
        private readonly int _size;
        private IList<IShip> _ships;

        public BattlefieldBuilder(int size)
        {
            _size = size;
        }

        public BattlefieldBuilder WithShips(IList<IShip> ships)
        {
            _ships = ships;

            return this;
        }

        public Battlefield Build()
        {
            var battlefield = new Battlefield(_size);
            var random = new Random();
            var randomWrapper = new RandomWrapper(random);
            var shipPlacer = new RandomShipPlacer(new RandomDirection(randomWrapper), new RandomPosition(randomWrapper), 10);

            for (var i = 0; i < _ships.Count; i++)
            {
                if (!shipPlacer.AddShip(battlefield, _ships[i]))
                {
                    throw new Exception("Failed to build battlefield");
                }
            }
            return battlefield;
        }
    }
}