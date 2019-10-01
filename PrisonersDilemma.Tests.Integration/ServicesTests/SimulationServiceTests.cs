using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.Tests.Integration.Common;
using PrisonersDilemma.Tests.Integration.Strategies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.ServicesTests
{
    [TestClass]
    public class SimulationServiceTests
    {
        private ISimulationService simulationService;
        private IStrategyRepository strategyRepository;

        [TestInitialize]
        public void Init()
        {
            MongoTestConventions.RegisterConventions();
            if (simulationService == null)
            {
                simulationService = TestContainer.BuildContainer().Resolve<ISimulationService>();
            }
            if (strategyRepository == null)
            {
                strategyRepository = TestContainer.BuildContainer().Resolve<IStrategyRepository>();
            }
        }

        [TestMethod]
        public async Task Get_Cooperator_Players_Strategies()
        {
            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }
            players = await simulationService.GetPlayersStrategies(players);

            bool badPlayers = players.Where(p => p.StrategyId != cooperator.Id).Any();
            Assert.IsFalse(badPlayers);
        }

        [TestMethod]
        public async Task Run_Once_When_Consistent()
        {
            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }

            Simulation simulation = await simulationService.Run(players);

            Assert.IsNotNull(simulation.Winner);
            Assert.AreEqual(1, simulation.PopulationsCompleated);
        }

        [TestMethod]
        public async Task Winner_Score_Is_Total_Score()
        {
            IGameSettingsProvider gameSettingsProvider = TestContainer.BuildContainer().Resolve<IGameSettingsProvider>();
            GameSettings gameSettings = gameSettingsProvider.GetGameSettings();
            int bothCooperate = 3;

            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }
            Simulation simulation = await simulationService.Run(players);

            int totalScore = gameSettings.TotalRounds * bothCooperate * (players.Count - 1);
            Assert.AreEqual(totalScore, simulation.Winner.Score);
        }

    }
}
