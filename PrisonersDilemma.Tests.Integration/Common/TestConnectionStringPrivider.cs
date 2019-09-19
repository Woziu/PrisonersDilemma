using PrisonersDilemma.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            return "PrisonersDilemmaDev";
        }

        public string GetSimulationCollectionName()
        {
            return "Simulations";
        }

        public string GetStrategyCollectionName()
        {
            return "Strategies";
        }
    }
}
