using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;

namespace PrisonersDilemma.Logic.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IStrategyService _strategyService;

        public GameService(IGameRepository gameRepository, IStrategyService strategyService)
        {
            _gameRepository = gameRepository;
            _strategyService = strategyService;
        }
        public Task<Game> Play(Player firstPlayer, Player secondPlayer, int totalRounds)
        {
            throw new NotImplementedException();
        }

        //TODO: use repository?
        public Task SaveGame(string populationId)
        {
            throw new NotImplementedException();
        }
    }
}
