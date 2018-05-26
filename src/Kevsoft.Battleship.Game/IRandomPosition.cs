using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public interface IRandomPosition
    {
        (char x, int y) Next(IEnumerable<(char x, int y)> possible);
    }
}