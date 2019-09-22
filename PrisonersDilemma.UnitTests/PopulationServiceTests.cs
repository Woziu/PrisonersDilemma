using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PrisonersDilemma.UnitTests
{
    [TestClass]
    public class PopulationServiceTests
    {
        [TestMethod]
        public void Scores_Are_Correct_After_Evaluation()
        {
            var gameServiceMock = new Mock<IGameService>();

            var rounds = new List<Round>();
            for (int i = 0; i < 10; i++)
            {
                rounds.Add(new Round() { FirstPlayerScore = 2, SecondPlayerScore = -1 });
            }

            gameServiceMock.Setup(g => g.Play(It.IsAny<Player>(), It.IsAny<Player>()))
                .Returns(new Game() { Rounds = rounds });

            var populationService = new PopulationService(gameServiceMock.Object);

            Player firstPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString()
            };
            Player secondPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString()
            };

            Population population = populationService.Evaluate(new List<Player>() { firstPlayer, secondPlayer });

            Player newFirstPlayer = population.Players.Where(p => p.Id == firstPlayer.Id).FirstOrDefault();
            Player newSecondPlayer = population.Players.Where(p => p.Id == secondPlayer.Id).FirstOrDefault();

            Assert.IsTrue(firstPlayer.Score == 20);
            Assert.AreEqual(-10, newSecondPlayer.Score);
        }

        [TestMethod]
        public void Population_Is_Consistent()
        {
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = "1" });
            }

            bool isConsistent = populationService.IsPopulationConsistent(new Population() { Players = players });
            Assert.IsTrue(isConsistent);
        }

        [TestMethod]
        public void Population_Is_Not_Consistent()
        {
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player() { StrategyId = "1" });
            }
            players.Add(new Player() { StrategyId = "2" });

            bool isConsistent = populationService.IsPopulationConsistent(new Population() { Players = players });
            Assert.IsFalse(isConsistent);
        }

        [TestMethod]
        public void New_Population_Is_Consistent()
        {
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 9; i++)
            {
                players.Add(new Player() { StrategyName = "a", StrategyId = "1", Score = 21 });
            }
            players.Add(new Player() { StrategyName = "b", StrategyId = "2", Score = 20 });
            Population population = populationService.GetNewPopulation(new Population() { Players = players });
            bool isConsistent = populationService.IsPopulationConsistent(population);
            Assert.IsTrue(isConsistent);
        }

        [TestMethod]
        public void New_Population_Players_Count_Did_Not_Change()
        {
            var gameServiceMock = new Mock<IGameService>();

            var populationService = new PopulationService(gameServiceMock.Object);

            var players = new List<Player>();
            for (int i = 0; i < 9; i++)
            {
                players.Add(new Player() { StrategyName = "a", StrategyId = "1", Score = 21 });
            }
            players.Add(new Player() { StrategyName = "b", StrategyId = "2", Score = 20 });

            Population population = populationService.GetNewPopulation(new Population() { Players = players });

            bool isConsistent = populationService.IsPopulationConsistent(population);
            Assert.IsTrue(isConsistent);
            Assert.AreEqual(players.Count, population.Players.Count);
        }
    }
}
