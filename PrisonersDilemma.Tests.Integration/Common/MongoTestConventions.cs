using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Tests.Integration
{
    public class MongoTestConventions : IConventionPack
    {
        private static bool conventionsRegistred = false;

        public IEnumerable<IConvention> Conventions => new List<IConvention>()
        {
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };

        public static void RegisterConventions()
        {
            if (!conventionsRegistred)
            {
                ConventionRegistry.Register("CustomConventions", new MongoTestConventions(), x => true);
                conventionsRegistred = true;
            }
        }
    }
}
