using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Helpers
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return "";
            throw new NotImplementedException();
        }

        public string GetDatabase()
        {
            throw new NotImplementedException();
        }

        public string GetSimulaionCollectionName()
        {
            throw new NotImplementedException();
        }

        public string GetStrategyCollectionName()
        {
            throw new NotImplementedException();
        }
    }
}
