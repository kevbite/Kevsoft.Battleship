using System;

namespace Kevsoft.Battleship.Game
{
    public class BattleshipGame 
    {
        public void Fire()
        {
            throw new System.NotImplementedException();
        }

        internal void Fire((char x, int y) move)
        {
            throw new NotImplementedException();
        }
        
        public bool IsComplete { get; }
    }
}