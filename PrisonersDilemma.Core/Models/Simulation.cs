using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    class Simulation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int SimulationsLimit { get; set; }
        public int SimulationsCompleated { get; set; }
    }
}
