using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System.Collections.Generic;

namespace PrisonersDilemma.GUI
{
    public class MongoConventions : IConventionPack
    {
        private static bool conventionsRegistred = false;

        public IEnumerable<IConvention> Conventions => new List<IConvention>()
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                //TODO: https://github.com/Lockbox-stack/Lockbox/tree/master/src/Lockbox.Api/MongoDb
                new CamelCaseElementNameConvention()
            };

        public static void RegisterConventions()
        {
            if (!conventionsRegistred)
            {
                ConventionRegistry.Register("CustomConventions", new MongoConventions(), x => true);
                conventionsRegistred = true;
            }
        }
    }
}
