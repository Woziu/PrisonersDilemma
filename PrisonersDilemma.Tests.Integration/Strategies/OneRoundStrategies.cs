using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Tests.Integration.Strategies
{
    public class OneRoundStrategies
    {
        public static Strategy GetCopycat()
        {
            var strategy = new Strategy()
            {
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
            return strategy;
        }

        public static Strategy GetCheaterCopycat()
        {
            var strategy = new Strategy()
            {
                Name = "Cheater Copycat",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = MoveType.Cheat
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
            return strategy;
        }

        public static Strategy GetReverser()
        {
            var strategy = new Strategy()
            {
                Name = "Reverser",
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
                        MoveType = MoveType.Cooperate
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
                        MoveType = MoveType.Cheat
                    },
                }
            };
            return strategy;
        }

        public static Strategy GetCheaterReverser()
        {
            var strategy = new Strategy()
            {
                Name = "Cheater Reverser",
                Moves = new List<Move>()
                {
                    new Move()
                    {
                        MoveType = MoveType.Cheat
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
                        MoveType = MoveType.Cooperate
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
                        MoveType = MoveType.Cheat
                    },
                }
            };
            return strategy;
        }
    }
}
