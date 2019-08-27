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
        private readonly ISimulationRepository _simulationRepository;
        private readonly IStrategyRepository _strategyRepository;
        private readonly int _simulationsLimit;
        
        public SimulationService(ISimulationRepository simulationRepository, IPopulationService populationService, 
            IStrategyRepository strategyRepository, List<Player> players, int simulationsLimit)
        {
            _populationService = populationService;
            _simulationRepository = simulationRepository;
            _strategyRepository = strategyRepository;
            _simulationsLimit = simulationsLimit;
        }
        public Task Run()
        {
            //save players list to DB??

            //TODO: get players strategies form DB

            //loop
            //evaluate
            //get new population

            //save winner to DB?
            throw new NotImplementedException();
        }
    }
}
