using System;
using System.Collections.Generic;
using System.Linq;
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

        public PopulationService(IPopulationRepository populationRepository, IGameService gameService)
        {
            _populationRepository = populationRepository;
            _gameService = gameService;            
        }
        
        public async Task<Population> Evaluate(List<Player> players)
        {
            Population population = new Population()
            {
                Id = Guid.NewGuid().ToString(),
                Games = new List<Game>()
            };
            //play
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = i + 1; j < players.Count; j++)
                {
                    Player firstPlayer = players[i];
                    Player secondPlayer = players[j];
                    //play game between players
                    Game game = await _gameService.PlayAsync(firstPlayer, secondPlayer);
                    //add score
                    //TODO: this looks suspicious, verify with intergration tests
                    players[i].Score += game.Rounds.Sum(r => r.FirstPlayerScore);
                    players[j].Score += game.Rounds.Sum(r => r.SecondPlayerScore);
                    //add game to population
                    population.Games.Add(game);
                }
            }
            //add new players to population
            population.Players = players;
            //TODO: save population
            return population;
        }

        public Task<Population> GetNewPopulation(Population population)
        {
            List<Player> newPlayersList = new List<Player>();
            int totalScore = population.Players.Sum(p => p.Score);
            Dictionary<string, int> scorePerStrategy = GetScorePerStrategy(population);

            foreach (KeyValuePair<string, int> strategyScore in scorePerStrategy)
            {
                double percentPerStrategy = ((double)strategyScore.Value / (double)totalScore) * 100.0;
                int newStrategyCount = (int)Math.Ceiling(population.Players.Count * (percentPerStrategy / 100));
                //if only 1 left make population consistent
                if (newStrategyCount + 1 == population.Players.Count)
                {
                    newStrategyCount++;
                }
                //add first player with giver strategy to list               
                Player playerToAdd = population.Players.Where(p => p.StrategyId == strategyScore.Key).FirstOrDefault();
                for (int i = 0; i < newStrategyCount; i++)
                {
                    newPlayersList.Add(playerToAdd);
                }
                if (newPlayersList.Count == population.Players.Count)
                {
                    break;
                }
            }

            return Task.FromResult(new Population() { Players = newPlayersList });
        }

        public Task<bool> IsConsistent(Population population)
        {
            string firstStrategy = population.Players[0].StrategyId;
            foreach (Player player in population.Players)
            {
                if (player.StrategyId != firstStrategy)
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(true);
        }

        private Dictionary<string, int> GetScorePerStrategy(Population population)
        {           
            Dictionary<string, int> scorePerStrategy = new Dictionary<string, int>();
            //sum score of each strategy
            foreach (Player player in population.Players)
            {
                if (scorePerStrategy.ContainsKey(player.StrategyId))
                {
                    scorePerStrategy[player.StrategyId] += player.Score;
                }
                else
                {
                    scorePerStrategy.Add(player.StrategyId, player.Score);
                }
            }
            return scorePerStrategy;
        }
    }
}
