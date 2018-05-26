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
            var battlefield = new Battlefield(10);
            var shipPlacer = new ShipPlacer(battlefield);
            shipPlacer.AddShip(new Battleship(new SingleLineBattleshipPlacement(1)), new ShipPlacement('A', 1, Direction.Across));
            var battleshipGame = new BattleshipGame(battlefield, null);

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
