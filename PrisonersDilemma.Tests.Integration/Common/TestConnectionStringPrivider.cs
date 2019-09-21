using PrisonersDilemma.Core.Helpers;
using System.IO;

namespace PrisonersDilemma.Tests.Integration
{
    public class TestConnectionPrivider : IConnectionStringProvider
    {
        private readonly string _connectionString;
        private TestConnectionPrivider() { }

        public TestConnectionPrivider(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                _connectionString = sr.ReadToEnd();
            }
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public string GetDatabase()
        {
            return "PrisonersDilemmaTests";
        }

        public string GetSimulationCollectionName()
        {
            return "SimulationsTests";
        }

        public string GetStrategyCollectionName()
        {
            return "StrategiesTests";
        }
    }
}
