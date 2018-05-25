namespace Kevsoft.Battleship.Game
{
    public class OccupiedBattlefieldCell : IBattlefieldCell
    {
        public bool HasShipPlaced { get; } = true;
    }
}