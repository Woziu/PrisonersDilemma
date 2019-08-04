using PrisonersDilemma.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Round
    {
        public int Id { get; set; }
        public List<PlayerMove> PlayersMoves { get; set; }        
        public int FirstPlayerScore { get; set; }
        public int SecondPlayerScore { get; set; }
    }
}
