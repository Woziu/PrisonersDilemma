using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Simulation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int PopulationsLimit { get; set; }
        public int PopulationsCompleated { get; set; }
        public List<Player> EntryPlayers { get; set; }
        public Player Winner { get; set; }
        public List<Population> Populations { get; set; }
    }
}
