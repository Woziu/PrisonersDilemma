using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.UnitTests.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonersDilemma.UnitTests
{
    [TestClass]
    public class SimulationServiceTests
    {
        [TestMethod]
        public async Task Finish_After_First_Consistent_Simulation()
        {
            string strategyId = Guid.NewGuid().ToString();
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyServiceMock = new Mock<IStrategyService>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.Evaluate(It.IsAny<List<Player>>()))
                .Returns((List<Player> p) => Task.FromResult(new Population() { Players = p }));
            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(true);

            strategyServiceMock.Setup(x => x.GetStrategiesById(It.IsAny<List<string>>()))
                .Returns(new List<Strategy>() { GetCoopStrategy(strategyId) });

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { PoplationsLimit = 10 });

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyServiceMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);
            
            Assert.AreEqual(1, simulation.PopulationsCompleated);
        }

        [TestMethod]
        public void Get_Players_Strategies()
        {
            var players = new List<Player>();
            var strategies = new List<Strategy>();
            for (int i = 0; i < 10; i++)
            {
                string id = Guid.NewGuid().ToString();
                players.Add(new Player() { StrategyId = id });
                strategies.Add(GetCoopStrategy(id));
            }

            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyServiceMock = new Mock<IStrategyService>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(true);

            strategyServiceMock.Setup(x => x.GetStrategiesById(It.IsAny<List<string>>()))
                .Returns(strategies);

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { PoplationsLimit = 10 });
            
            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyServiceMock.Object, settingsProviderMock.Object);

            List<Player> newPlayers = simulationService.GetPlayersStrategies(players);

            strategyServiceMock.Verify(x => x.GetStrategiesById(It.IsAny<List<string>>()), Times.Exactly(1));            
            //no assert required since Verify throws exception if fails
        }        

        [TestMethod]
        public async Task Consistent_Strategy_Wins()
        {
            string strategyId = Guid.NewGuid().ToString();
            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyServiceMock = new Mock<IStrategyService>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.Evaluate(It.IsAny<List<Player>>()))
                .Returns((List<Player> p) => Task.FromResult(new Population() { Players = p }));
            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(true);

            strategyServiceMock.Setup(x => x.GetStrategiesById(It.IsAny<List<string>>()))
                .Returns(new List<Strategy>() { GetCoopStrategy(strategyId) });

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { PoplationsLimit = 10 });           

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyServiceMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);

            Assert.AreEqual(strategyId, simulation.Winner.StrategyId);
        }

        [TestMethod]
        public async Task No_Winner_If_Not_Consistent()
        {
            string strategyId = Guid.NewGuid().ToString();
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyServiceMock = new Mock<IStrategyService>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(false);

            populationServiceMock.Setup(x => x.Evaluate(It.IsAny<List<Player>>()))
                .Returns((List<Player> p) => Task.FromResult(new Population() { Players = p }));

            populationServiceMock.Setup(x => x.GetNewPopulation(It.IsAny<Population>()))
                .Returns((Population p) => new Population() { Players = p.Players });

            strategyServiceMock.Setup(x => x.GetStrategiesById(It.IsAny<List<string>>()))
                .Returns(new List<Strategy>() { GetCoopStrategy(strategyId) });

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { PoplationsLimit = 10 });

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyServiceMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);

            Assert.IsNull(simulation.Winner);            
        }

        [TestMethod]
        public void Throw_Exception_When_No_Players()
        {
            var simulationRepositoryMock = new Mock<ISimulationRepository>();            
            var strategyServiceMonk = new Mock<IStrategyService>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyServiceMonk.Object, settingsProviderMock.Object);

            var ex = Assert.ThrowsExceptionAsync<ArgumentNullException>( () => simulationService.Run(new List<Player>()));
            Assert.IsNotNull(ex);
        }

        public static Strategy GetCoopStrategy(string strategyId)
        {
            var strategy = new Strategy()
            {
                Id = strategyId,
                Name = "Simple Cooprerator",
                Moves = new List<Move>()
                {
                    new Move() { MoveType = MoveType.Cooperate }
                }
            };

            return strategy;
        }

    }
}
