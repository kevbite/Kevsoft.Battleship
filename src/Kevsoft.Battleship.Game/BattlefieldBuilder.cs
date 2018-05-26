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
            var shipPlacer = new ShipPlacer(battlefield);

            for (var i = 0; i < _ships.Count; i++)
            {
                shipPlacer.AddShip(_ships[i], new ShipPlacement('X', i + 1, Direction.Across));
            }
            return battlefield;
        }
    }
}