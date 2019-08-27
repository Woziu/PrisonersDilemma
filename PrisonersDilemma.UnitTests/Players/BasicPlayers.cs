using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.UnitTests.Players
{
    public class BasicPlayers
    {
        public static Player GetCoopPlayer()
        {
            var strategy = new Strategy()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Simple Cooprerator",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = Core.Enums.MoveType.Cooperate
                    }
                }
            };
            var player = new Player()
            {
                Strategy = strategy,
                StrategyId = strategy.Id,
                Score = 0,
                Id = Guid.NewGuid().ToString()
            };

            return player;
        }

        public static Player GetCheaterPlayer()
        {
            var strategy = new Strategy()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Simple Cheater",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = Core.Enums.MoveType.Cheat
                    }
                }
            };
            var player = new Player()
            {
                Strategy = strategy,
                StrategyId = strategy.Id,
                Score = 0,
                Id = Guid.NewGuid().ToString()
            };

            return player;
        }
    }
}
