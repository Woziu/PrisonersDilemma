using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrisonersDilemma.Logic.Services
{
    public class PopulationService : IPopulationService
    {
        private readonly IGameService _gameService;
        private readonly SimulationSettings _simulationSettins;

        public PopulationService(IGameService gameService, 
            ISimulationSettingsProvider simulationSettingsProvider)
        {
            _gameService = gameService;
            _simulationSettins = simulationSettingsProvider.GetSimulationSettings();
        }

        public Population Evaluate(List<Player> players)
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
                    Game game = _gameService.Play(firstPlayer, secondPlayer);
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

        public Population GetNewPopulation(Population population)
        {
            List<Player> newPlayersList = new List<Player>();
            int totalScore = population.Players.Sum(p => p.Score);
            Dictionary<string, int> scorePerStrategy = GetScorePerStrategy(population);

            int minScore = scorePerStrategy.Values.Min();
            string bestStrategyName = scorePerStrategy
                .Where(s => s.Value == scorePerStrategy.Values.Max())
                .FirstOrDefault().Key;

            Player mutatedPlayer = population.Players
                    .Where(p => String.Equals(p.StrategyName, bestStrategyName))
                    .FirstOrDefault();

            int mutationsCount = 0;

            foreach (KeyValuePair<string, int> strategyScore in scorePerStrategy)
            {
                double percentPerStrategy = ((double)strategyScore.Value / (double)totalScore) * 100.0;
                int newStrategyCount = (int)Math.Round(population.Players.Count * (percentPerStrategy / 100));

                bool canMutate = strategyScore.Value == minScore ? true : false;

                //add first player with giver strategy to list               
                Player playerWithCurrentStrategy = population.Players
                    .Where(p => String.Equals(p.StrategyName, strategyScore.Key))
                    .FirstOrDefault();
                Player playerToAdd = playerWithCurrentStrategy;

                var randomNumer = new Random();

                for (int i = 0; i < newStrategyCount; i++)
                {
                    if (newPlayersList.Count + 1 > population.Players.Count)
                    {
                        break;
                    }
                    try
                    {
                        if (canMutate && randomNumer.Next(99) < _simulationSettins.MutationChancePercent)
                        {
                            playerToAdd = mutatedPlayer;
                            mutationsCount++;
                        }
                        Player newPlayer = new Player()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Score = playerToAdd.Score,
                            Strategy = playerToAdd.Strategy,
                            StrategyId = playerToAdd.StrategyId,
                            StrategyName = playerToAdd.StrategyName
                        };
                        newPlayersList.Add(newPlayer);
                        playerToAdd = playerWithCurrentStrategy;
                    }
                    catch (Exception ex)
                    {

                    }
                }                
            }            
            return new Population() { Players = newPlayersList, MutationsCount = mutationsCount };
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

        public Dictionary<string, int> GetScorePerStrategy(Population population)
        {
            Dictionary<string, int> scorePerStrategy = new Dictionary<string, int>();
            //sum score of each strategy
            try
            {
                foreach (Player player in population.Players)
                {
                    if (scorePerStrategy.ContainsKey(player.StrategyName))
                    {
                        scorePerStrategy[player.StrategyName] += player.Score;
                    }
                    else
                    {
                        scorePerStrategy.Add(player.StrategyName, player.Score);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return scorePerStrategy;
        }
    }
}
