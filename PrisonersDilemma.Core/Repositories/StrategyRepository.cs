using MongoDB.Driver;
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
        private readonly IMongoCollection<Strategy> _strategies;

        public StrategyRepository(IConnectionStringProvider connectionStringProvider)
        {
            var client = new MongoClient(connectionStringProvider.GetConnectionString());
            var database = client.GetDatabase(connectionStringProvider.GetDatabase());
            _strategies = database.GetCollection<Strategy>(connectionStringProvider.GetStrategyCollectionName());
        }

        public string Add(Strategy strategy)
        {
            throw new NotImplementedException();
        }
        public async Task<string> AddAsync(Strategy strategy)
        {
            await _strategies.InsertOneAsync(strategy);
            return strategy.Id;
        }            
        public Strategy Get(string id) =>
            _strategies.Find<Strategy>(s => s.Id == id).FirstOrDefault();
        public List<Strategy> Get(List<string> idList) =>
            _strategies.Find<Strategy>(s => idList.Contains(s.Id)).ToList();         
        public async Task<Strategy> GetAsync(string id) =>
            await _strategies.FindSync<Strategy>(s => s.Id == id).FirstOrDefaultAsync();
    }
}
