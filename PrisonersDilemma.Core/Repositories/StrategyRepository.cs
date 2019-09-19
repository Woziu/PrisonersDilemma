using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public class StrategyRepository : IStrategyRepository
    {
        private readonly IMongoCollection<Strategy> _strategies;
        public StrategyRepository(IConnectionStringProvider connectionStringProvider)
        {
            var client = new MongoClient(connectionStringProvider.GetConnectionString());
            var database = client.GetDatabase(connectionStringProvider.GetDatabase());
            _strategies = database.GetCollection<Strategy>(connectionStringProvider.GetStrategyCollectionName());
        }

        public async Task<string> AddAsync(Strategy strategy)
        {
            await _strategies.InsertOneAsync(strategy);
            return strategy.Id;
        }
        public async Task<List<Strategy>> GetAll() =>
            await _strategies.AsQueryable().ToListAsync();
        public async Task<Strategy> GetAsync(string id) =>
            await _strategies.AsQueryable().FirstOrDefaultAsync(s => s.Id == id);
        public async Task<List<Strategy>> GetAsync(List<string> idList) =>
            await _strategies.AsQueryable().Where(s => idList.Contains(s.Id)).ToListAsync();
        public async Task<Strategy> GetByNameAsync(string strategyName) =>
            await _strategies.AsQueryable().FirstOrDefaultAsync(s => s.Name == strategyName);//TODO:removable - only for testing
    }
}
