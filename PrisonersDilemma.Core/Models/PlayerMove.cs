using PrisonersDilemma.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class PlayerMove
    {        
        public string PlayerId { get; set; }
        public MoveType Type;
    }
}
