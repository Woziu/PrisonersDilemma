﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace PrisonersDilemma.Core.Helpers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["ConnectionString"];
        }

        public string GetDatabase()
        {
            return ConfigurationManager.AppSettings["DatabaseName"];
        }

        public string GetSimulationCollectionName()
        {
            return ConfigurationManager.AppSettings["SimulationsCollection"];
        }

        public string GetStrategyCollectionName()
        {
            return ConfigurationManager.AppSettings["StrategiesCollection"];
        }
    }
}
