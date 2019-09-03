using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;

namespace PrisonersDilemma.Core.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;

        public GameRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }
        public Task SaveGameAsync(string populationId, Game game)
        {
            throw new NotImplementedException();
        }
    }
}
