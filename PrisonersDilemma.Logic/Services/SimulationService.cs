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
        //TODO: PlayerManager/PlayerService 
        public SimulationService(ISimulationRepository simulationRepository, IPopulationService populationService, 
            List<Player> players, int simulationsLimit)//TODO: IoC?
        {
            _populationService = populationService;
        }
        public Task Run()
        {
            //save players list to DB??
            
            //loop
            //evaluate
            //get new population
            
            //save winner to DB?
            throw new NotImplementedException();
        }
    }
}
