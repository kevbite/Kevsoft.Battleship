using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public interface IBattleshipPlacement
    {
        IEnumerable<(char x, int y)> GetShipPositions(ShipPlacement placement);
    }
}