using PrisonersDilemma.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Repositories
{
    public class PopulationRepository : IPopulationRepository
    {
        private readonly string _connectionString;

        public PopulationRepository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionString = connectionStringProvider.GetConnectionString();
        }
    }
}
