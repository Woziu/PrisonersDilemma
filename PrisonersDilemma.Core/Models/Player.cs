using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string StrategyId { get; set; }
        public int Score { get; set; }
    }
}
