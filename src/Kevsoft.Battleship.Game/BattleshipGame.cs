﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Kevsoft.Battleship.Game
{
    public class BattleshipGame : IReadOnlyBattleshipGame
    {
        private readonly IBattlefield _battlefield;
        private readonly HashSet<(char x, int y)> _shots = new HashSet<(char x, int y)>();

        public BattleshipGame(IBattlefield battlefield)
        {
            _battlefield = battlefield;
        }

        public FireResult Fire((char x, int y) position)
        {
            return new FireResult(_shots.Add(position));
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
    }
}