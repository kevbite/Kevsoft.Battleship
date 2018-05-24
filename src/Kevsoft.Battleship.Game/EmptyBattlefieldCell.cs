namespace Kevsoft.Battleship.Game
{
    public class EmptyBattlefieldCell : IBattlefieldCell
    {
        public bool HasShipPlaced { get; } = false;
    }
}