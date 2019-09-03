using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public class PopulationRepository : IPopulationRepository
    {
        private readonly string _connectionString;

        public PopulationRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }

        public async Task SavePopulationAsync(string simulationId, Population population)
        {
            throw new NotImplementedException();
        }
    }
}
