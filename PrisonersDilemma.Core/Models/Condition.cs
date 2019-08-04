using PrisonersDilemma.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Condition
    {
        public int Id { get; set; }
        public MoveType PlayerMove { get; set; }
        public MoveType EnemyMove { get; set; }
    }
}
