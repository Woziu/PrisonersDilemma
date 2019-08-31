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
        public List<Round> GetCoopHistory(string cooperatePlayerId, string cheaterPlayerId)
        {
            var rounds = new List<Round>();

            List<PlayerMove> moves = new List<PlayerMove>()
            {
                new PlayerMove(){ PlayerId = cooperatePlayerId, Type = MoveType.Cooperate },
                new PlayerMove(){ PlayerId = cheaterPlayerId, Type = MoveType.Cheat }
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

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id, Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cooperate, move.Type);
        }

        [TestMethod]
        public async Task Always_Cheat()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCheaterPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id, Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Copycat_Should_Cheat()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCopycatPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id, Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Copycat_Should_Cooperate()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = BasicPlayers.GetCheaterPlayer();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(player.Id, Guid.NewGuid().ToString()));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Should_Cheat_Vs_Cooperator()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = ConditionalPlayers.GetCheaterVsCooperator();

            PlayerMove move = await strategyService.GetNextMoveAsync(player, GetCoopHistory(Guid.NewGuid().ToString(), player.Id));
            Assert.AreEqual(MoveType.Cheat, move.Type);
        }

        [TestMethod]
        public async Task Should_Coop_Vs_Cheater()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = ConditionalPlayers.GetCheaterVsCooperator();
            string enemyId = Guid.NewGuid().ToString();
            List<PlayerMove> moves = new List<PlayerMove>()
            {
                new PlayerMove(){ PlayerId = player.Id, Type = MoveType.Cooperate },
                new PlayerMove(){ PlayerId = enemyId, Type = MoveType.Cheat }
            };
            var rounds = new List<Round>()
            {
                new Round() { PlayersMoves = moves },
                new Round() { PlayersMoves = moves },
            };

            PlayerMove move = await strategyService.GetNextMoveAsync(player, rounds);

            Assert.AreEqual(MoveType.Cooperate, move.Type);
        }

        [TestMethod]
        public async Task Should_Cheat_Vs_Cheater()
        {
            var repositoryMock = new Mock<IStrategyRepository>();
            StrategyService strategyService = new StrategyService(repositoryMock.Object);
            Player player = ConditionalPlayers.GetCheaterVsCheater();
            string enemyId = Guid.NewGuid().ToString();
            List<PlayerMove> moves = new List<PlayerMove>()
            {
                new PlayerMove(){ PlayerId = player.Id, Type = MoveType.Cooperate },
                new PlayerMove(){ PlayerId = enemyId, Type = MoveType.Cheat }
            };
            var rounds = new List<Round>()
            {
                new Round() { PlayersMoves = moves },
                new Round() { PlayersMoves = moves },
                new Round() { PlayersMoves = moves },
            };

            PlayerMove move = await strategyService.GetNextMoveAsync(player, rounds);

            Assert.AreEqual(MoveType.Cheat, move.Type);
        }        
    }
}
