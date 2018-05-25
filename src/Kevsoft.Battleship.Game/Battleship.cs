namespace Kevsoft.Battleship.Game
{
    public class Battleship : IShip
    {
        private readonly IBattleshipPlacement _battleshipPlacement;

        public Battleship(IBattleshipPlacement battleshipPlacement)
        {
            _battleshipPlacement = battleshipPlacement;
        }

        public void Place(IBattlefield battlefield, ShipPlacement placement)
        {
            var shipPositions = _battleshipPlacement.GetShipPositions(placement);

            foreach (var shipPosition in shipPositions)
            {
                battlefield.MarkCell(shipPosition);
            }
        }

        public bool CanPlace(IBattlefield battlefield, ShipPlacement placement)
        {
            var shipPositions = _battleshipPlacement.GetShipPositions(placement);

            foreach (var shipPosition in shipPositions)
            {
                if (battlefield.Cells.TryGetValue(shipPosition, out var cell))
                {
                    if (cell.HasShipPlaced)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}