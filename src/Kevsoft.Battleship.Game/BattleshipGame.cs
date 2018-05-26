using System;
using System.Collections.Generic;
using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class BattleshipGame : IReadOnlyBattleshipGame
    {
        private readonly IBattlefield _battlefield;
        private readonly IShotValidator _shotValidator;
        private readonly IGameStatisticsCalculator _gameStatisticsCalculator;
        private readonly HashSet<(char x, int y)> _shots = new HashSet<(char x, int y)>();

        public BattleshipGame(IBattlefield battlefield, IShotValidator shotValidator, IGameStatisticsCalculator gameStatisticsCalculator)
        {
            _battlefield = battlefield;
            _shotValidator = shotValidator;
            _gameStatisticsCalculator = gameStatisticsCalculator;
        }

        public FireResult Fire((char x, int y) position)
        {
            if (_shotValidator.Validate(position, _battlefield))
            {
                return new FireResult(_shots.Add(position));
            }

            return new FireResult(false);
        }

        public bool IsComplete
        {
            get
            {
                return _battlefield.Cells.All(battlefieldCell => !battlefieldCell.Value.HasShipPlaced
                                                                 || _shots.Contains(battlefieldCell.Key));
            }
        }

        public ISet<(char x, int y)> Cells => _battlefield.Cells.Keys.ToHashSet();

        public ISet<(char x, int y)> Hits => _shots
            .Intersect(_battlefield.Cells.Where(x => x.Value.HasShipPlaced).Select(x => x.Key)).ToHashSet();

        public ISet<(char x, int y)> Misses => _shots
            .Except(Hits).ToHashSet();

        public GameStatistics CurrentStatistics => _gameStatisticsCalculator.GetCurrentStatistics(this);
    }
}