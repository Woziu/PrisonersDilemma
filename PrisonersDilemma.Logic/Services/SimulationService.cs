using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            bool isPopulationConsistent = false;

            Simulation simulation = new Simulation()
            {
                StartDate = DateTime.Now,
                PopulationsLimit = _simulationSettings.PoplationsLimit,
                EntryPlayers = players,
                Populations = new List<Population>()
            };
            await _simulationRepository.SaveAsync(simulation);

            players = GetPlayersStrategies(players);

            do
            {
                currentPopulation++;
                //evaluate players in current population
                Population population = await _populationService.Evaluate(players);
                //check if consistent
                isPopulationConsistent = await _populationService.IsPopulationConsistent(population);

                population.IsConsistent = isPopulationConsistent;
                simulation.Populations.Add(population);

                if (isPopulationConsistent)
                {
                    break;
                }
                else
                {
                    //get players for next population
                    Population newPopulation = await _populationService.GetNewPopulation(population);
                    players = newPopulation.Players;
                }                
            }
            while (currentPopulation < _simulationSettings.PoplationsLimit);

            simulation.FinishDate = DateTime.Now;
            simulation.PopulationsCompleated = currentPopulation;
            simulation.Winner = isPopulationConsistent ? players.FirstOrDefault() : null;

            await _simulationRepository.UpdateAsync(simulation);

            return simulation;
        }

        public List<Player> GetPlayersStrategies(List<Player> players)
        {
            //TODO: probably should be tested
            //limit db connections by getting each strategy only oncefa
            List<string> distinctStrategies = players
                .Where(p => p.Strategy == null)
                .Select(s => s.StrategyId)
                .Distinct().ToList();

            Dictionary<string, Strategy> strategies = _strategyService
                .GetStrategiesById(distinctStrategies)
                .Distinct()
                .ToDictionary(k => k.Id, k => k);
                        
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Strategy == null)
                {
                    players[i].Strategy = strategies[players[i].StrategyId];
                }
            }
            return players;
        }
    }
}
