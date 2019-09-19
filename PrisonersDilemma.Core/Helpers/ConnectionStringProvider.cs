using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrisonersDilemma.Core.Helpers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _connectionString;
        public ConnectionStringProvider(string fileName)
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

        public string GetSimulaionCollectionName()
        {
            return "Simulations";
        }

        public string GetStrategyCollectionName()
        {
            return "Strategies"; 
        }
    }
}
