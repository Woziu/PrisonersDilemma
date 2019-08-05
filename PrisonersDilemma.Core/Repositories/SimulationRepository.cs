using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;

namespace PrisonersDilemma.Core.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly string _connectionString;

        public SimulationRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }

        public Task AddAsync(Simulation simulation)
        {
            throw new NotImplementedException();
        }

        public Task<Simulation> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Simulation simulation)
        {
            throw new NotImplementedException();
        }
    }
}
