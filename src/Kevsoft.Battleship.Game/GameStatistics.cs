namespace Kevsoft.Battleship.Game
{
    public class GameStatistics
    {
        public GameStatistics(int hits, int misses, int totalShots, double accuracy)
        {
            Hits = hits;
            Misses = misses;
            TotalShots = totalShots;
            Accuracy = accuracy;
        }

        public int Hits { get; }

        public int Misses { get; }

        public int TotalShots { get; }

        public double Accuracy { get; }
    }
}
