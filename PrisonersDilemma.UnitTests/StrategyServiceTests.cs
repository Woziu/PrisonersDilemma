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
    public class StrategyServiceTests
    {
        public List<Round> GetCoopHistory(string cooperatePlayerId)
        {
            string otherPlayerId = Guid.NewGuid().ToString();
            var rounds = new List<Round>();

            List<PlayerMove> moves = new List<PlayerMove>()
            {
                new PlayerMove(){ PlayerId = cooperatePlayerId, Type = MoveType.Cooperate },
                new PlayerMove(){ PlayerId = otherPlayerId, Type = MoveType.Cheat }
            };

            for (int i = 0; i < 10; i++)
            {
                rounds.Add(new Round() { Id = i, PlayersMoves = moves });
            }

            return rounds;
        }

        [TestMethod]
        public async Task Always_Cooparate()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCoopPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id));
            Assert.AreEqual(MoveType.Cooperate, move.Type);
        }

        [TestMethod]
        public async Task Always_Cheat()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCheaterPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Copycat_Should_Cheat()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCopycatPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Copycat_Should_Cooperate()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCheaterPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        //TODO: ConditionOperator tests
    }
}
