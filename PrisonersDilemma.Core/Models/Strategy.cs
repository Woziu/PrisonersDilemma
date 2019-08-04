using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Strategy
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Move> Moves { get; set; }
    }
}
