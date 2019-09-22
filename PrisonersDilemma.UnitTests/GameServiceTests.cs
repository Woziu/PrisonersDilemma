using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Settings;
using PrisonersDilemma.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrisonersDilemma.UnitTests//TODO:Rename project to PrisonersDilemma.Tests.Unit? when bored
{
    [TestClass]
    public class GameServiceTests
    {
        private readonly int MoveModifier = -1;
        private readonly int CooperateModifier = 3;
        private readonly int TotalRounds = 10;

        [TestMethod]
        public void Max_Score_When_Cooperate()
        {
            GameService gameService = GetBasicMockedCoopStrategyServices();

            Game game = gameService.Play(new Player(), new Player());

            int firstPlayerTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            Assert.AreEqual(TotalRounds * CooperateModifier, firstPlayerTotalScore);
        }

        [TestMethod]
        public void Equal_Score_When_Cooperate()
        {
            GameService gameService = GetBasicMockedCoopStrategyServices();

            Game game = gameService.Play(new Player(), new Player());

            int firstPlayerTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int secondPlayerTotalScoure = game.Rounds.Sum(s => s.SecondPlayerScore);
            Assert.AreEqual(firstPlayerTotalScore, secondPlayerTotalScoure);
        }

        [TestMethod]
        public void Equal_Score_When_Cheat()
        {
            GameService gameService = GetBasicMockedCheatStrategyServices();

            Game game = gameService.Play(new Player(), new Player());

            int firstPlayerTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int secondPlayerTotalScoure = game.Rounds.Sum(s => s.SecondPlayerScore);
            Assert.AreEqual(firstPlayerTotalScore, secondPlayerTotalScoure);
        }

        [TestMethod]
        public void Score_Equal_50_When_Always_Cheat()
        {
            var strategyMock = new Mock<IStrategyService>();
            var gameSettingsMock = new Mock<IGameSettingsProvider>();

            gameSettingsMock.Setup(x => x.GetGameSettings()).Returns(GetTestSettings());

            strategyMock.Setup(x => x.GetNextMove(It.IsAny<Player>(), It.IsAny<List<Round>>()))
                .Returns((Player p, List<Round> r) => new PlayerMove()
                {
                    PlayerId = p.Id,
                    Type = p.StrategyName == "Simple Cheater" ? MoveType.Cheat : MoveType.Cooperate
                });

            var gameService = new GameService(strategyMock.Object, gameSettingsMock.Object);
            var cheater = new Player() { StrategyName = "Simple Cheater" };
            var cooperator = new Player() { StrategyName = "Simple Cooperator" };

            Game game = gameService.Play(cheater, cooperator);

            int cheaterTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int cooperatorTotalScoure = game.Rounds.Sum(s => s.SecondPlayerScore);
            Assert.AreEqual(50, cheaterTotalScore);
            Assert.IsTrue(cooperatorTotalScoure == 0);
        }

        [TestMethod]
        public void Score_Equal_10_When_Both_Cheat()
        {
            GameService gameService = GetBasicMockedCheatStrategyServices();

            Game game = gameService.Play(new Player(), new Player());

            int firstPlayerScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(s => s.SecondPlayerScore);
            Assert.AreEqual(firstPlayerScore, secondPlayerScore);
            Assert.IsTrue(firstPlayerScore == 10);
        }

        [TestMethod]
        public void Score_Equal_30_When_Both_Cooperate()
        {
            GameService gameService = GetBasicMockedCoopStrategyServices();

            Game game = gameService.Play(new Player(), new Player());

            int firstPlayerScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(s => s.SecondPlayerScore);
            Assert.AreEqual(firstPlayerScore, secondPlayerScore);
            Assert.IsTrue(firstPlayerScore == 30);
        }


        public GameService GetBasicMockedCoopStrategyServices()
        {
            var strategyMock = new Mock<IStrategyService>();
            var gameSettingsMock = new Mock<IGameSettingsProvider>();

            strategyMock.Setup(x => x.GetNextMove(It.IsAny<Player>(), It.IsAny<List<Round>>()))
                .Returns((Player p, List<Round> r) => (new PlayerMove() { PlayerId = p.Id, Type = MoveType.Cooperate }));

            gameSettingsMock.Setup(x => x.GetGameSettings()).Returns(GetTestSettings());

            return new GameService(strategyMock.Object, gameSettingsMock.Object);
        }

        public GameService GetBasicMockedCheatStrategyServices()
        {
            var strategyMock = new Mock<IStrategyService>();
            var gameSettingsMock = new Mock<IGameSettingsProvider>();

            strategyMock.Setup(x => x.GetNextMove(It.IsAny<Player>(), It.IsAny<List<Round>>()))
                .Returns((Player p, List<Round> r) => new PlayerMove() { PlayerId = p.Id, Type = MoveType.Cheat });

            gameSettingsMock.Setup(x => x.GetGameSettings()).Returns(GetTestSettings());

            return new GameService(strategyMock.Object, gameSettingsMock.Object);
        }

        private Game GetSampleGame()
        {
            Player firstPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Score = 0,
                StrategyId = Guid.NewGuid().ToString()
            };

            Player secondPlayer = new Player()
            {
                Id = Guid.NewGuid().ToString(),
                Score = 0,
                StrategyId = Guid.NewGuid().ToString()
            };

            return new Game()
            {
                //Id = Guid.NewGuid().ToString(),
                FirstPlayer = firstPlayer,
                SecondPlayer = secondPlayer,
                Rounds = null

            };
        }
        private GameSettings GetTestSettings()
        {
            return new GameSettings()
            {
                MoveModifier = this.MoveModifier,
                CooperateModifier = this.CooperateModifier,
                TotalRounds = this.TotalRounds
            };
        }
    }
}
