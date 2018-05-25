using System;
using System.Collections.Generic;
using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class BattleshipGame 
    {
        private readonly IBattlefield _battlefield;
        private readonly HashSet<(char x, int y)> _shots = new HashSet<(char x, int y)>();

        public BattleshipGame(IBattlefield battlefield)
        {
            _battlefield = battlefield;
        }

        public void Fire((char x, int y) position)
        {
            _shots.Add(position);
        }

        public bool IsComplete
        {
            get
            {
                return _battlefield.Cells.All(battlefieldCell => !battlefieldCell.Value.HasShipPlaced
                                                                 || _shots.Contains(battlefieldCell.Key));
            }
        }
    
    }
}