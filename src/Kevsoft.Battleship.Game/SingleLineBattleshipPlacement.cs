using System.Collections.Generic;
using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class SingleLineBattleshipPlacement : IBattleshipPlacement
    {
        private readonly int _battleshipSize;

        public SingleLineBattleshipPlacement(int battleshipSize)
        {
            _battleshipSize = battleshipSize;
        }

        public IEnumerable<(char x, int y)> GetShipPositions(ShipPlacement placement)
        {
            if (placement.Direction == Direction.Across)
            {
                return Enumerable.Range(0, _battleshipSize)
                    .Select(x => ((char)(placement.X + x), placement.Y))
                    .ToList();
            }
            else
            {
                return Enumerable.Range(0, _battleshipSize)
                    .Select(x => (placement.X, placement.Y + x))
                    .ToList();
            }
        }
    }
}