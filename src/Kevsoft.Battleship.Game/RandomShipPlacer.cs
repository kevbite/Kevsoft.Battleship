using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class RandomShipPlacer
    {
        private readonly IRandomDirection _randomDirection;
        private readonly IRandomPosition _randomPosition;
        private readonly int _maxAttempts;

        public RandomShipPlacer(IRandomDirection randomDirection, IRandomPosition randomPosition, int maxAttempts)
        {
            _randomDirection = randomDirection;
            _randomPosition = randomPosition;
            _maxAttempts = maxAttempts;
        }

        public bool AddShip(IBattlefield battlefield, IShip ship)
        {
            for (var i = 0; i < _maxAttempts; i++)
            {
                var placement = NextShipPlacement(battlefield);

                if (ship.CanPlace(battlefield, placement))
                {
                    ship.Place(battlefield, placement);

                    return true;
                }
            }

            return false;
        }

        private ShipPlacement NextShipPlacement(IBattlefield battlefield)
        {
            var direction = _randomDirection.Next();
            var position = _randomPosition.Next(battlefield.Cells.Keys.ToList());

            var placement = new ShipPlacement(position.x, position.y, direction);
            return placement;
        }
    }
}