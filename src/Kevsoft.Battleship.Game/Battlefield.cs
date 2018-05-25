using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class Battlefield : IBattlefield
    {
        private readonly Dictionary<(char x, int y), IBattlefieldCell> _cells;

        public Battlefield(int size)
        {
            _cells = BuildCells(size);
        }

        public IReadOnlyDictionary<(char x, int y), IBattlefieldCell> Cells => _cells;

        public void MarkCell((char x, int y) positions)
        {
            _cells[positions] = new OccupiedBattlefieldCell();
        }

        private static Dictionary<(char x, int y), IBattlefieldCell> BuildCells(int size)
        {
            var battlefieldCells = new Dictionary<(char x, int y), IBattlefieldCell>(size * size);
            for (var x = 0; x < size; x++)
            {
                for (var y = 1; y <= size; y++)
                {
                    var letter = (char)('A' + x);
                    battlefieldCells.Add((letter, y), new EmptyBattlefieldCell());
                }
            }

            return battlefieldCells;
        }

    }
}