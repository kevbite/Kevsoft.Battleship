using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public interface IReadOnlyBattleshipGame
    {
        ISet<(char x, int y)> Cells { get; }

        ISet<(char x, int y)> Hits { get; }

        ISet<(char x, int y)> Misses { get; }

        bool IsComplete { get; }
    }
}