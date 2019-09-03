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
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(Task.FromResult(true));

            strategyRepositoryMock.Setup(x => x.GetStrategyById(It.IsAny<string>()))
                .Returns(GetCoopStrategy(strategyId));

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { SimulationsLimit = 10 });

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);
            
            Assert.AreEqual(1, simulation.SimulationsCompleated);
        }

        [TestMethod]
        public async Task Get_Distinct_Strategy_For_Every_Player()
        {
            string strategyId = Guid.NewGuid().ToString();
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(Task.FromResult(true));

            strategyRepositoryMock.Setup(x => x.GetStrategyById(It.IsAny<string>()))
                .Returns(GetCoopStrategy(strategyId));

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { SimulationsLimit = 10 });

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = Guid.NewGuid().ToString() });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);

            List<Player> newPlayers = simulationService.GetPlayersStrategies(players);

            strategyRepositoryMock.Verify(x => x.GetStrategyById(It.IsAny<string>()), Times.Exactly(players.Count));            
            //no assert required since Verify throws exception if fails
        }
        [TestMethod]
        public async Task Get_Only_Distinct_Strategies()
        {            
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(Task.FromResult(true));

            strategyRepositoryMock.Setup(x => x.GetStrategyById(It.IsAny<string>()))
                .Returns((string s) => GetCoopStrategy(s));

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { SimulationsLimit = 10 });

            int distinctStrategies = 5;
            List<Player> players = new List<Player>();
            for (int i = 0; i < distinctStrategies; i++)
            {
                string strategyId = Guid.NewGuid().ToString();
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);

            List<Player> newPlayers = simulationService.GetPlayersStrategies(players);

            strategyRepositoryMock.Verify(x => x.GetStrategyById(It.IsAny<string>()), Times.Exactly(distinctStrategies));
            //no assert required since Verify throws exception if fails
        }

        [TestMethod]
        public async Task Consistent_Strategy_Wins()
        {
            string strategyId = Guid.NewGuid().ToString();
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(Task.FromResult(true));           

            strategyRepositoryMock.Setup(x => x.GetStrategyById(It.IsAny<string>()))
                .Returns(GetCoopStrategy(strategyId));

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { SimulationsLimit = 10 });
            
            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);

            Assert.AreEqual(strategyId, simulation.Winner.StrategyId);
        }

        [TestMethod]
        public async Task No_Winner_If_Not_Consistent()
        {
            string strategyId = Guid.NewGuid().ToString();
            var simulationRepositoryMock = new Mock<ISimulationRepository>();
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            populationServiceMock.Setup(x => x.IsPopulationConsistent(It.IsAny<Population>()))
                .Returns(Task.FromResult(false));

            populationServiceMock.Setup(x => x.Evaluate(It.IsAny<List<Player>>()))
                .Returns((List<Player> p) => Task.FromResult(new Population() { Players = p }));

            populationServiceMock.Setup(x => x.GetNewPopulation(It.IsAny<Population>()))
                .Returns((Population p) => Task.FromResult(new Population() { Players = p.Players }));

            strategyRepositoryMock.Setup(x => x.GetStrategyById(It.IsAny<string>()))
                .Returns(GetCoopStrategy(strategyId));

            settingsProviderMock.Setup(x => x.GetSimulationSettings())
                .Returns(new SimulationSettings() { SimulationsLimit = 10 });

            List<Player> players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = strategyId });
            }

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);

            Simulation simulation = await simulationService.Run(players);

            Assert.IsNull(simulation.Winner);            
        }

        [TestMethod]
        public async Task Throw_Exception_When_No_Players()
        {
            var simulationRepositoryMock = new Mock<ISimulationRepository>();            
            var strategyRepositoryMock = new Mock<IStrategyRepository>();
            var populationServiceMock = new Mock<IPopulationService>();
            var settingsProviderMock = new Mock<ISimulationSettingsProvider>();

            SimulationService simulationService = new SimulationService(simulationRepositoryMock.Object,
                populationServiceMock.Object, strategyRepositoryMock.Object, settingsProviderMock.Object);
           //TODO:fix this
            var ex = Assert.ThrowsException<ArgumentNullException>( () => simulationService.Run(new List<Player>()));
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
                    new Move()
                    {
                        MoveType = MoveType.Cooperate
                    }
                }
            };

            return strategy;
        }

    }
}
