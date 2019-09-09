using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Simulation
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int SimulationsLimit { get; set; }
        public int SimulationsCompleated { get; set; }
        public List<Player> EntryPlayers { get; set; }
        public Player Winner { get; set; }
        public List<Population> Populations { get; set; }
    }
}
