using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string StrategyId { get; set; }
        public int Score { get; set; }
        public Strategy Strategy { get; set; }//dont store in db
    }
}
