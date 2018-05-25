namespace Kevsoft.Battleship.Game
{
    public class ShipPlacement
    {
        public char X { get; }

        public int Y { get; }

        public Direction Direction { get; }

        public ShipPlacement(char x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}