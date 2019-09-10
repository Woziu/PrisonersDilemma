using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Tests.Integration.Strategies
{
    public class NoMemoryStrategies
    {
        public static Strategy GetSimpleCheater()
        {
            var strategy = new Strategy()
            {
                Name = "Simple Cheater",
                Moves = new List<Move>()
                {
                    new Move() { MoveType = MoveType.Cheat }
                }
            };
            return strategy;
        }

        public static Strategy GetSimpleCooperator()
        {
            var strategy = new Strategy()
            {
                Name = "Simple Cooperator",
                Moves = new List<Move>()
                {
                    new Move() { MoveType = MoveType.Cooperate }
                }
            };
            return strategy;
        }
    }
}
