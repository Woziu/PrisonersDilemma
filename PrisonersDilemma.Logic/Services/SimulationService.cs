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
        private readonly IPopulationService _populationService;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IStrategyRepository _strategyRepository;
        private readonly SimulationSettings _simulationSettings;
               
        public SimulationService(ISimulationRepository simulationRepository, IPopulationService populationService, 
            IStrategyRepository strategyRepository, ISimulationSettingsProvider simulationSettingsProdiver)
        {
            _populationService = populationService;
            _simulationRepository = simulationRepository;
            _strategyRepository = strategyRepository;
            _simulationSettings = simulationSettingsProdiver.GetSimulationSettings();
        }
        public async Task<Simulation> Run(List<Player> players)
        {
            if (players == null || !players.Any())
            {
                throw new ArgumentNullException("No players supplied");
            }
            int currentSimulation = 0;            
            bool isPopulationConsistent = false;

            Simulation simulation = new Simulation()
            {
                Id = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                SimulationsLimit = _simulationSettings.SimulationsLimit,
                EntryPlayers = players
            };

            players = GetPlayersStrategies(players);

            do
            {
                currentSimulation++;
                //evaluate players in current population
                Population population = await _populationService.Evaluate(players);
                //save to db
                await _populationService.SavePopulationAsync(simulation.Id, population);
                //check if consistent
                isPopulationConsistent = await _populationService.IsPopulationConsistent(population);                
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
            while (currentSimulation < _simulationSettings.SimulationsLimit);

            simulation.FinishDate = DateTime.Now;
            simulation.SimulationsCompleated = currentSimulation;
            simulation.Winner = isPopulationConsistent ? players.FirstOrDefault() : null;

            await _simulationRepository.SaveSimulationAsync(simulation);

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

            var strategies = new Dictionary<string, Strategy>();
            //TODO: probalby should be simplified
            //TODO: test vs async get all
            foreach(string strategyId in distinctStrategies)
            {
                strategies[strategyId] = _strategyRepository.GetStrategyById(strategyId);                 
            }
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
