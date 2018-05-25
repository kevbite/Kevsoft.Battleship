using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class Battlefield : IBattlefield
    {
        public Battlefield(int size)
        {
            Cells = BuildCells(size);
        }

        public IReadOnlyDictionary<(char x, int y), IBattlefieldCell> Cells { get; }

        public void MarkCell((char x, int y) positions)
        {
            throw new System.NotImplementedException();
        }

        private static IReadOnlyDictionary<(char x, int y), IBattlefieldCell> BuildCells(int size)
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