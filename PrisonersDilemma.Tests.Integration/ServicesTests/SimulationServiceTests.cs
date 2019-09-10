using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.Tests.Integration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.ServicesTests
{
    [TestClass]
    public class SimulationServiceTests
    {
        IConnectionStringProvider connection;
        [TestInitialize]
        public void GetConnectionString()
        {
            if (connection == null)
            {
                MongoTestConventions.RegisterConventions();
                connection = new TestConnectionPrivider("connection.txt");
            }
        }

        [TestMethod]
        public async Task Get_Cooperator_Players_Strategies()
        {
            var strategyRepository = new StrategyRepository(connection);
            var strategyService = new StrategyService(strategyRepository);
            var simulationRepository = new SimulationRepository(connection);
            var gameService = new GameService(strategyService, new GameSettingsProvider());
            var populationService = new PopulationService(gameService);
            //TODO: simplify /\
            var simulationServce = new SimulationService(simulationRepository, populationService,
                strategyService, new SimulationSettingsProvider());
            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }
            players = simulationServce.GetPlayersStrategies(players);

            bool badPlayers = players.Where(p => p.StrategyId != cooperator.Id).Any();
            Assert.IsFalse(badPlayers);
        }

        [TestMethod]
        public async Task Run_Once_When_Consistent()
        {
            var strategyRepository = new StrategyRepository(connection);
            var strategyService = new StrategyService(strategyRepository);
            var simulationRepository = new SimulationRepository(connection);
            var gameService = new GameService(strategyService, new GameSettingsProvider());
            var populationService = new PopulationService(gameService);
            //TODO: simplify /\
            var simulationServce = new SimulationService(simulationRepository, populationService,
                strategyService, new SimulationSettingsProvider());
            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }

            Simulation simulation = await simulationServce.Run(players);

            Assert.IsNotNull(simulation.Winner);
            Assert.AreEqual(1, simulation.PopulationsCompleated);
        }

        [TestMethod]
        public async Task Winner_Score_Is_Greater_Than_Zero()
        {
            var strategyRepository = new StrategyRepository(connection);
            var strategyService = new StrategyService(strategyRepository);
            var simulationRepository = new SimulationRepository(connection);
            var gameService = new GameService(strategyService, new GameSettingsProvider());
            var populationService = new PopulationService(gameService);
            //TODO: simplify /\
            var simulationServce = new SimulationService(simulationRepository, populationService,
                strategyService, new SimulationSettingsProvider());
            Strategy cooperator = await strategyRepository
                .GetByNameAsync(NoMemoryStrategies.GetSimpleCooperator().Name);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = cooperator.Id });
            }

            Simulation simulation = await simulationServce.Run(players);

            Assert.IsTrue(simulation.Winner.Score > 0);//TODO: check equal score
        }

    }
}
