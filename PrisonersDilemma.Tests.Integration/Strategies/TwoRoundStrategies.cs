using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System.Collections.Generic;

namespace PrisonersDilemma.Tests.Integration.Strategies
{
    public class TwoRoundStrategies
    {
        public static Strategy GetGoodStrategy()
        {
            var strategy = new Strategy()
            {
                Name = "Strategia A",
                Moves = new List<Move>()
                {
                    //Depth 0
                    new Move()
                    {
                        MoveType = MoveType.Cheat
                    },
                    //Depth 1
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 100,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            }
                        },
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
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
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
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    //Depth 2
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 100,
                        ConditionsOperator = ConditionOperator.OR,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 2,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    }
                }
            };
            return strategy;
        }

        public static Strategy GetStrategyB()
        {
            var strategy = new Strategy()
            {
                Name = "Strategy B",
                Moves = new List<Move>()
                {
                    //Depth 0
                    new Move()
                    {
                        MoveType = MoveType.Cooperate
                    },
                    //Depth 1
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 100,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 100,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
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
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    //Depth 2
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 50,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 100,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    }
                }
            };
            return strategy;
        }

        public static Strategy GetStrategyC()
        {
            var strategy = new Strategy()
            {
                Name = "Strategy C",
                Moves = new List<Move>()
                {
                    //Depth 0
                    new Move()
                    {
                        MoveType = MoveType.Cooperate
                    },
                    //Depth 1
                    new Move()
                    {
                        TotalDepth = 1,
                        MoveType = MoveType.Cheat
                    },                    
                    //Depth 2
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 50,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 50,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 50,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 99,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                PlayerMove = MoveType.Cooperate,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                PlayerMove = MoveType.Cheat,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    }
                }
            };
            return strategy;
        }
        public static Strategy GetStrategyX()
        {
            var strategy = new Strategy()
            {
                Name = "Łagodny wet za wet",
                Moves = new List<Move>()
                {
                    //Depth 0
                    new Move()
                    {
                        MoveType = MoveType.Cooperate
                    },
                    //Depth 1
                    new Move()
                    {
                        TotalDepth = 1,
                        Priority = 10,
                        MoveType = MoveType.Cooperate
                    },                    
                    //Depth 2
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 100,
                        ConditionsOperator = ConditionOperator.AND,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cheat
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 20,
                        ConditionsOperator = ConditionOperator.OR,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cheat
                            },
                            new Condition()
                            {
                                Depth = 2,
                                EnemyMove = MoveType.Cheat
                            }
                        },
                        MoveType = MoveType.Cooperate
                    },
                    new Move()
                    {
                        TotalDepth = 2,
                        Priority = 20,
                        ConditionsOperator = ConditionOperator.OR,
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Depth = 1,
                                EnemyMove = MoveType.Cooperate
                            },
                            new Condition()
                            {
                                Depth = 2,
                                EnemyMove = MoveType.Cooperate
                            }
                        },
                        MoveType = MoveType.Cooperate
                    }
                }
            };
            return strategy;
        }
    }
}
