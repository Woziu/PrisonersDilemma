using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Game
    {
        public string Id { get; set; }
        public List<Round> Rounds { get; set; }
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }       
    }
}
