using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Population
    {
        public int Id { get; set; }
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }        
        public bool IsConsistent { get; set; }
    }
}
