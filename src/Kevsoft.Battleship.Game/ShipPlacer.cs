namespace Kevsoft.Battleship.Game
{
    public class ShipPlacer
    {
        private readonly IBattlefield _battlefield;

        public ShipPlacer(IBattlefield battlefield)
        {
            _battlefield = battlefield;
        }

        public bool AddShip(IShip ship, ShipPlacement placement)
        {
            if (!ship.CanPlace(_battlefield, placement))
            {
                return false;
            }

            ship.Place(_battlefield, placement);
            return true;
        }
    }
}