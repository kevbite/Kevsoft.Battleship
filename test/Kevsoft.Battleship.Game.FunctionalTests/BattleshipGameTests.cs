using System.Linq;
using Xunit;

namespace Kevsoft.Battleship.Game.FunctionalTests
{
    public class BattleshipGameTests
    {
        [Fact]
        public void ShouldWinAfterPlayingAllMoves()
        {
            var allMoves = AllAcrossMoves.SelectMany(x => AllDownMoves.Select(y => (x, y)));
            var battleshipGame = new BattleshipGame();

            foreach (var move in allMoves)
            {
                battleshipGame.Fire(move);

            }

            Assert.True(battleshipGame.IsComplete);
        }

        private static readonly char[] AllAcrossMoves = new char[]
        {
            'A','B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'
        };

        private static readonly int[] AllDownMoves = new int[]
        {
            1,2,3,4,5,6,7,8,9,10
        };
    }
}
