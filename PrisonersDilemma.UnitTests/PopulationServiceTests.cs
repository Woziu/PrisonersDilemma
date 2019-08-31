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
    public class PopulationServiceTests
    {
        [TestMethod]
        public async Task Scores_Are_Correct_After_Evaluation()
        {
            var repositoryMock = new Mock<IPopulationRepository>();
            var gameServiceMock = new Mock<IGameService>();

            var rounds = new List<Round>();
            for (int i = 0; i < 10; i++)
            {
                rounds.Add(new Round() { FirstPlayerScore = 2, SecondPlayerScore = -1 });
            }

            gameServiceMock.Setup(g => g.PlayAsync(It.IsAny<Player>(), It.IsAny<Player>()))
                .Returns(Task.FromResult(new Game() { Rounds = rounds }));

            var populationService = new PopulationService(repositoryMock.Object, gameServiceMock.Object);

            Player firstPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString()
            };
            Player secondPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString()
            };

            Population population = await populationService.Evaluate(new List<Player>() { firstPlayer, secondPlayer });

            Player newFirstPlayer = population.Players.Where(p => p.Id == firstPlayer.Id).FirstOrDefault();
            Player newSecondPlayer = population.Players.Where(p => p.Id == secondPlayer.Id).FirstOrDefault();

            Assert.IsTrue(firstPlayer.Score == 20);
            Assert.AreEqual(-10, newSecondPlayer.Score);
        }

        [TestMethod]
        public async Task Population_Is_Consistent()
        {
            var repositoryMock = new Mock<IPopulationRepository>();
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(repositoryMock.Object, gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = "1" });
            }
            
            bool isConsistent = await populationService.IsConsistent(new Population() { Players = players });        
            Assert.IsTrue(isConsistent); 
        }

        [TestMethod]
        public async Task Population_Is_Not_Consistent()
        {
            var repositoryMock = new Mock<IPopulationRepository>();
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(repositoryMock.Object, gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = "1" });
            }
            players.Add(new Player() { StrategyId = "2" });            

            bool isConsistent = await populationService.IsConsistent(new Population() { Players = players });
            Assert.IsFalse(isConsistent);
        }

        [TestMethod]
        public async Task New_Population_Is_Consistent()
        {
            var repositoryMock = new Mock<IPopulationRepository>();
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(repositoryMock.Object, gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 9; i++)
            {
                players.Add(new Player() { StrategyId = "1", Score = 21 });
            }
            players.Add(new Player() { StrategyId = "2", Score = 20 });
            Population population = await populationService.GetNewPopulation(new Population() { Players = players });
            bool isConsistent = await populationService.IsConsistent(population);
            Assert.IsTrue(isConsistent);
        }

        [TestMethod]
        public async Task New_Population_Players_Count_Did_Not_Change()
        {
            var repositoryMock = new Mock<IPopulationRepository>();
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(repositoryMock.Object, gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 9; i++)
            {
                players.Add(new Player() { StrategyId = "1", Score = 21 });
            }
            players.Add(new Player() { StrategyId = "2", Score = 20 });
            
            Population population = await populationService.GetNewPopulation(new Population() { Players = players });

            bool isConsistent = await populationService.IsConsistent(population);
            Assert.IsTrue(isConsistent);
            Assert.AreEqual(players.Count, population.Players.Count);            
        }      
    }
}
