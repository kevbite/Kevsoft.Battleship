using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class BattlefieldBuilder
    {
        private readonly IBattlefield _battlefield;

        public BattlefieldBuilder(IBattlefield battlefield)
        {
            _battlefield = battlefield;
        }

        public bool AddShip(IShip ship)
        {
            if (!ship.CanPlace(_battlefield))
            {
                return false;
            }

            ship.Place(_battlefield);
            return true;
        }
    }

    public interface IShip
    {
        void Place(IBattlefield battlefield);

        bool CanPlace(IBattlefield battlefield);
    }

    public class Battlefield : IBattlefield
    {
        public Battlefield(int size)
        {
            Cells = BuildCells(size);
        }

        public IReadOnlyDictionary<(char x, int y), IBattlefieldCell> Cells { get; }
    
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