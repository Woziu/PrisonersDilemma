using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;

namespace PrisonersDilemma.Core.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly IMongoCollection<Simulation> _simulations;
        public SimulationRepository(IConnectionStringProvider connectionStringProvider)
        {
            var client = new MongoClient(connectionStringProvider.GetConnectionString());
            var database = client.GetDatabase(connectionStringProvider.GetDatabase());
            _simulations = database.GetCollection<Simulation>(connectionStringProvider.GetSimulationCollectionName());
        }
        public async Task<Simulation> GetAsync(string id) =>
            await _simulations.AsQueryable().FirstOrDefaultAsync<Simulation>(s => s.Id == id);

        public async Task<string> SaveAsync(Simulation simulation)
        {
            await _simulations.InsertOneAsync(simulation);
            return simulation.Id;
        }
        public async Task UpdateAsync(Simulation simulation) =>
            await _simulations.ReplaceOneAsync(s => s.Id == simulation.Id, simulation);


    }
}
