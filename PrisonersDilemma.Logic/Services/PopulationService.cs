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
        private readonly IGameService _gameService;

        public PopulationService(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        public async Task<Population> Evaluate(List<Player> players)
        {
            Population population = new Population()
            {
                Id = Guid.NewGuid().ToString(),
                Games = new List<Game>()
            };
            //TODO: check if async would be faster
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
                    players[i].Score += game.Rounds.Sum(r => r.FirstPlayerScore);
                    players[j].Score += game.Rounds.Sum(r => r.SecondPlayerScore);
                    //add game to population
                    population.Games.Add(game);
                }
            }           
            //add new players to population
            population.Players = players;            
            return population;
        }

        public Population GetNewPopulation(Population population)//TODO: make sync
        {
            List<Player> newPlayersList = new List<Player>();
            int totalScore = population.Players.Sum(p => p.Score);
            Dictionary<string, int> scorePerStrategy = GetScorePerStrategy(population);

            foreach (KeyValuePair<string, int> strategyScore in scorePerStrategy)
            {
                double percentPerStrategy = ((double)strategyScore.Value / (double)totalScore) * 100.0;
                int newStrategyCount = (int)Math.Floor(population.Players.Count * (percentPerStrategy / 100));
                //if only 1 left make population consistent
                if (newStrategyCount + 1 == population.Players.Count)//TODO: probably should rethink this
                {
                    newStrategyCount++;
                }
                //add first player with giver strategy to list               
                Player playerToAdd = population.Players.Where(p => p.StrategyId == strategyScore.Key).FirstOrDefault();
                for (int i = 0; i < newStrategyCount; i++)
                {
                    Player newPlayer = new Player()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Score = playerToAdd.Score,//TODO: what if first player has highest or lowest score
                        Strategy = playerToAdd.Strategy,
                        StrategyId = playerToAdd.StrategyId,
                        StrategyName = playerToAdd.StrategyName
                    };
                    newPlayersList.Add(newPlayer);
                }
                if (newPlayersList.Count > population.Players.Count)
                {
                    throw new IndexOutOfRangeException("Too much players");
                }
                if (newPlayersList.Count == population.Players.Count)
                {
                    break;
                }
            }
            
            return new Population() { Players = newPlayersList };
        }

        public bool IsPopulationConsistent(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException("There is no population");
            }
            string firstStrategy = population.Players[0].StrategyId;
            foreach (Player player in population.Players)
            {
                if (player.StrategyId != firstStrategy)
                {
                    return false;
                }
            }
            return true;
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
