using PrisonersDilemma.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Repositories
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly string _connectionString;

        public StrategyRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }
    }
}
