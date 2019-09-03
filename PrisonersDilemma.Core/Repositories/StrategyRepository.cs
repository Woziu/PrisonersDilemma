using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly string _connectionString;

        public StrategyRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }

        //TODO: StrategyRepository : calculate total depth if none
        //TODO: StrategyRepository : if priority == 0 > priority = depth

        public Strategy GetStrategyById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Strategy> GetStrategyByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
