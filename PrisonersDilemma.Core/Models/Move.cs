using PrisonersDilemma.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Move
    {
        public int TotalDepth;
        public int Priority { get; set; }
        public List<Condition> Conditions { get; set; }
        public ConditionOperator ConditionsOperator { get; set; }
        public MoveType MoveType { get; set; }        
    }
}
