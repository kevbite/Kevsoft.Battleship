using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public interface IBattlefield
    {
        IReadOnlyDictionary<(char x, int y), IBattlefieldCell> Cells { get; }

        void MarkCell((char x, int y) positions);
    }
}