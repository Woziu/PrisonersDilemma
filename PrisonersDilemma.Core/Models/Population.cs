using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PrisonersDilemma.Core.Models
{
    public class Population
    {
        public string Id { get; set; }
        [BsonIgnore]
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
        public bool IsConsistent { get; set; }
        public Dictionary<string, int> ScorePerStrategy { get; set; }
    }
}
