namespace Kevsoft.Battleship.Game
{
    public class ShipPlacer
    {
        public bool AddShip(IBattlefield battlefield, IShip ship, ShipPlacement placement)
        {
            if (!ship.CanPlace(battlefield, placement))
            {
                return false;
            }

            ship.Place(battlefield, placement);
            return true;
        }
    }
}