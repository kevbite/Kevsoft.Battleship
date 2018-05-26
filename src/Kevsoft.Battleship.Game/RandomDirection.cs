namespace Kevsoft.Battleship.Game
{
    public class RandomDirection :IRandomDirection
    {
        private readonly IRandom _random;

        public RandomDirection(IRandom random)
        {
            _random = random;
        }

        public Direction Next()
        {
            var value = _random.Next(0, 2);

            return value == 0 ? Direction.Across : Direction.Down;
        }
    }
}