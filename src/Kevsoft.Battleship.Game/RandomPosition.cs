using System.Collections.Generic;
using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class RandomPosition : IRandomPosition
    {
        private readonly IRandom _random;

        public RandomPosition(IRandom random)
        {
            _random = random;
        }

        public (char x, int y) Next(IEnumerable<(char x, int y)> possible)
        {
            var count = possible.Count();

            var next = _random.Next(0, count);

            return possible.ElementAt(next);
        }
    }
}