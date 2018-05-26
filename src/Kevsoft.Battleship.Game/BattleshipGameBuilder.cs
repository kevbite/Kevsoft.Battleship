using System;
using System.Collections.Generic;

namespace Kevsoft.Battleship.Game
{
    public class StandardBattleshipGameBuilder
    {
        private readonly List<IShip> _ships = new List<IShip>();

        private readonly Shipyard _shipyard = new Shipyard(new Dictionary<ShipType, Func<IShip>>()
        {
            {ShipType.Battleship, () => new Battleship(new SingleLineBattleshipPlacement(1)) },
            {ShipType.Destroyer, () => new Battleship(new SingleLineBattleshipPlacement(1)) },
        });

        public StandardBattleshipGameBuilder WithShip(ShipType type)
        {
            var ship = _shipyard.CreateShip(type);

            _ships.Add(ship);

            return this;
        }

        public BattleshipGame Build()
        {
            var battlefield = new Battlefield(10);
            var shipPlacer = new ShipPlacer(battlefield);

            for (var i = 0; i < _ships.Count; i++)
            {
                shipPlacer.AddShip(_ships[i], new ShipPlacement('X', i + 1, Direction.Across));
            }

            var validator = new PositionOnBattlefieldValidator();
            var statisticsCalculator = new GameStatisticsCalculator();

            return new BattleshipGame(battlefield, validator, statisticsCalculator);
        }
    }
}
