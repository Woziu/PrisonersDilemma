using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.UnitTests.Players
{
    public class ConditionalPlayers
    {
        public static Player GetCheaterVsCooperator()
        {
            var strategy = new Strategy()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cheat vs coop and coop vs cheater",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 1,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        ConditionsOperator = ConditionOperator.AND,
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 1,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cheat
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
