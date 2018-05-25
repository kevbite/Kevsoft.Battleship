namespace Kevsoft.Battleship.Game
{
    public interface IShip
    {
        void Place(IBattlefield battlefield, ShipPlacement placement);

        bool CanPlace(IBattlefield battlefield, ShipPlacement placement);
    }
}