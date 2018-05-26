namespace Kevsoft.Battleship.Game
{
    public class PositionOnBattlefieldValidator : IShotValidator
    {
        public bool Validate((char x, int y) position, IBattlefield battlefield)
        {
            return battlefield.Cells.ContainsKey(position);
        }
    }
}