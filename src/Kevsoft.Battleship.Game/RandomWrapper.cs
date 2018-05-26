using System;

namespace Kevsoft.Battleship.Game
{
    public class RandomWrapper : IRandom
    {
        private readonly Random _random;

        public RandomWrapper(Random random)
        {
            _random = random;
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}