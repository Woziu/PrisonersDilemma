using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly IPopulationService _populationService;

        public int SimulationsLimit { get; set; }

        public SimulationService(ISimulationRepository simulationRepository, IPopulationService populationService, List<Player> players, int simulationsLimit)
        {
            _populationService = populationService;
            this.SimulationsLimit = simulationsLimit;
        }
        public Task Run()
        {
            throw new NotImplementedException();
        }
    }
}
