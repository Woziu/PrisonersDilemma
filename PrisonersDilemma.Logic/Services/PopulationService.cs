using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;

namespace PrisonersDilemma.Logic.Services
{
    public class PopulationService : IPopulationService
    {
        private readonly IPopulationRepository _populationRepository;
        private readonly IGameService _gameService;        

        public PopulationService(IPopulationRepository populationRepository, IGameService gameService, 
            List<Player> players)
        {
            _populationRepository = populationRepository;
            _gameService = gameService;            
        }
        
        public Task Evaluate()
        {
            //play
            //save
            throw new NotImplementedException();
        }

        public Task<Population> GetNewPopulation()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsConsistent()
        {
            throw new NotImplementedException();
        }
    }
}
