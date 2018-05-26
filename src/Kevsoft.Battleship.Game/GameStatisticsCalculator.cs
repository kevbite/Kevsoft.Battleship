namespace Kevsoft.Battleship.Game
{
    public class GameStatisticsCalculator : IGameStatisticsCalculator
    {
        public GameStatistics GetCurrentStatistics(IReadOnlyBattleshipGame battleshipGame)
        {
            var hits = battleshipGame.Hits.Count;
            var misses = battleshipGame.Misses.Count;
            var totalShots = hits + misses;
            var accuracy = (double)hits / totalShots;

            return new GameStatistics(hits, misses, totalShots, accuracy);
        }
    }
}