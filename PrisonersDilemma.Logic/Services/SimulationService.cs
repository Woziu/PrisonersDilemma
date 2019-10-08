using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ISimulationRepository _simulationRepository;
        private readonly IPopulationService _populationService;
        private readonly IStrategyService _strategyService;
        private readonly SimulationSettings _simulationSettings;

        public SimulationService(ISimulationRepository simulationRepository, IPopulationService populationService,
            IStrategyService strategyService, ISimulationSettingsProvider simulationSettingsProdiver)
        {
            _populationService = populationService;
            _simulationRepository = simulationRepository;
            _strategyService = strategyService;
            _simulationSettings = simulationSettingsProdiver.GetSimulationSettings();
        }
        public async Task<Simulation> Run(List<Player> players)
        {
            if (players == null || !players.Any())
            {
                throw new ArgumentNullException("No players supplied");
            }
            int currentPopulation = 0;
            int mutationsCount = 0;
            bool isPopulationConsistent = false;

            Simulation simulation = new Simulation()
            {
                StartDate = DateTime.Now,
                PopulationsLimit = _simulationSettings.PoplationsLimit,
                EntryPlayers = players,
                Populations = new List<Population>()
            };
            await _simulationRepository.SaveAsync(simulation);

            players = await GetPlayersStrategies(players);

            do
            {
                currentPopulation++;
                //evaluate players in current population
                Population population = _populationService.Evaluate(players);
                //check if consistent
                population.IsConsistent = _populationService.IsPopulationConsistent(population);
                population.ScorePerStrategy = _populationService.GetScorePerStrategy(population);
                //add population to simulation
                simulation.Populations.Add(population);

                if (population.IsConsistent)
                {
                    players = population.Players;
                    isPopulationConsistent = true;
                    break;
                }
                else
                {
                    //get players for next population
                    Population newPopulation = _populationService.GetNewPopulation(population);
                    mutationsCount += newPopulation.MutationsCount;
                    players = newPopulation.Players;
                }
            }
            while (currentPopulation < _simulationSettings.PoplationsLimit);

            //save simulation
            simulation.FinishDate = DateTime.Now;
            simulation.PopulationsCompleated = currentPopulation;
            simulation.Winner = isPopulationConsistent ? players.FirstOrDefault() : null;
            simulation.MutationsCount = mutationsCount;
            await _simulationRepository.UpdateAsync(simulation);

            return simulation;
        }

        public async Task<List<Player>> GetPlayersStrategies(List<Player> players)
        {
            //limit db connections by getting each strategy only oncefa
            List<string> distinctStrategies = players
                .Where(p => p.Strategy == null)
                .Select(s => s.StrategyId)
                .Distinct().ToList();

            List<Strategy> strategies = await _strategyService
                .GetStrategiesById(distinctStrategies);

            Dictionary<string, Strategy> strategiesDict = strategies
                .Distinct().ToDictionary(k => k.Id, k => k);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Strategy == null)
                {
                    players[i].Strategy = strategiesDict[players[i].StrategyId];
                    players[i].StrategyName = strategiesDict[players[i].StrategyId].Name;
                }
            }
            return players;
        }
    }
}
