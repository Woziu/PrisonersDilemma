using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;

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
                        MoveType = MoveType.Cooperate
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
                        MoveType = MoveType.Cheat
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
        public static Player GetCopycatPlayer()
        {
            var strategy = new Strategy()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Copycat",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 1,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
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
