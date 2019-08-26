using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;
using PrisonersDilemma.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonersDilemma.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private readonly int MoveModifier = -1;
        private readonly int CooperateModifier = 3;

        private readonly int TotalRounds = 10;

        public GameService GetBasicMockedService()
        {
            var gameMock = new Mock<IGameRepository>();
            var strategyMock = new Mock<IStrategyService>();
            var gameSettingsMock = new Mock<IGameSettingsProvider>();

            //gameMock.Setup(x => x.GetAsync(It.IsAny<string>()))
            //    .Returns(Task.FromResult(GetSampleGame()));
            strategyMock.Setup(x => x.GetNextMoveAsync(It.IsAny<Player>(), It.IsAny<List<Round>>()))
                .Returns((Player p, List<Round> r) => (Task.FromResult(new PlayerMove() { PlayerId = p.Id, Type = MoveType.Cooperate })));

            gameSettingsMock.Setup(x => x.GetGameSettings()).Returns(GetTestSettings());

            return new GameService(gameMock.Object, strategyMock.Object, gameSettingsMock.Object);


        }

        [TestMethod]
        public async Task Max_Score_When_Cooperate()
        {                    
            GameService gameService = GetBasicMockedService();

            Game game = await gameService.PlayAsync(new Player(), new Player(), TotalRounds);

            int firstPlayerTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            Assert.AreEqual((TotalRounds * MoveModifier + TotalRounds * CooperateModifier), firstPlayerTotalScore);
        }

        [TestMethod]
        public async Task Equal_Score_When_Cooperate()
        {
            GameService gameService = GetBasicMockedService();

            Game game = await gameService.PlayAsync(new Player(), new Player(), TotalRounds);

            int firstPlayerTotalScore = game.Rounds.Sum(s => s.FirstPlayerScore);
            int secondPlayerTotalScoure = game.Rounds.Sum(s => s.SecondPlayerScore);           
            Assert.AreEqual(firstPlayerTotalScore, secondPlayerTotalScoure);           
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
                CooperateModifier = this.CooperateModifier
            };
        }
    }
}