namespace Kevsoft.Battleship.Game
{
    public interface IGameStatisticsCalculator
    {
        GameStatistics GetCurrentStatistics(IReadOnlyBattleshipGame battleshipGame);
    }
}