namespace Kevsoft.Battleship.Game.Tests
{
    public static class BattleshipGameExtentions
    {
        public static void FireAllCells(this BattleshipGame battleship)
        {
            foreach (var cell in battleship.Cells)
            {
                battleship.Fire(cell);
            }
        }
    }
}