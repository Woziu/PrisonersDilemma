using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string StrategyId { get; set; }
        public string StrategyName { get; set; }
        public int Score { get; set; }
        [BsonIgnore]
        public Strategy Strategy { get; set; }
    }
}
