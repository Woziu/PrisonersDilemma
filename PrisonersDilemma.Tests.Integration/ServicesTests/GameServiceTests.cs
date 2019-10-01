using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.Tests.Integration.Common;
using PrisonersDilemma.Tests.Integration.Strategies;
using System;
using System.Linq;

namespace PrisonersDilemma.Tests.Integration.ServicesTests
{
    [TestClass]
    public class GameServiceTests
    {
        private IGameService gameService;

        [TestInitialize]
        public void Init()
        {
            if (gameService == null)
            {
                gameService = TestContainer.BuildContainer().Resolve<IGameService>();
            }
        }

        [TestMethod]
        public void Cooperators_Scores_Are_Equal()
        {
            Player firstCooperator = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());
            Player secondCooperator = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());

            Game game = gameService.Play(firstCooperator, secondCooperator);

            int firstCooperatorScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondCooperatorScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstCooperatorScore == 30);
            Assert.AreEqual(firstCooperatorScore, secondCooperatorScore);
        }

        [TestMethod]
        public void GoodStrategies_Scores_Are_Equal()
        {
            Player firstPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());
            Player secondPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstPlayerScore == 26);
            Assert.AreEqual(firstPlayerScore, secondPlayerScore);
        }

        [TestMethod]
        public void Copycats_Scores_Are_Equal()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());
            Player secondPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(30, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void BadCopycats_Scores_Are_Equal()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());
            Player secondPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(10, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void Cheaters_Scores_Are_Equal()
        {
            Player firstPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(10, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void GoodStrategy_vs_Copycat_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());
            Player secondPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstPlayerScore == 20);
            Assert.AreEqual(15, secondPlayerScore);
        }

        [TestMethod]
        public void GoodStrategy_vs_Cheater_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstPlayerScore == 7);
            Assert.AreEqual(22, secondPlayerScore);
        }

        [TestMethod]
        public void GoodStrategy_vs_Cooperator_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstPlayerScore == 34);
            Assert.AreEqual(24, secondPlayerScore);
        }

        [TestMethod]
        public void GoodStrategy_vs_BadCopycat_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(TwoRoundStrategies.GetGoodStrategy());
            Player secondPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(16, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void Copycat_vs_BadCopycat_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());
            Player secondPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(25, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void Copycat_vs_Cooperator_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(30, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void Copycat_vs_Cheater_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCopycat());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(9, firstPlayerScore);
            Assert.IsTrue(secondPlayerScore == 14);
        }

        [TestMethod]
        public void BadCopycat_vs_Cheater_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(10, firstPlayerScore);
            Assert.IsTrue(firstPlayerScore == secondPlayerScore);
        }

        [TestMethod]
        public void BadCopycat_vs_Cooperator_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(OneRoundStrategies.GetCheaterCopycat());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.AreEqual(32, firstPlayerScore);
            Assert.IsTrue(secondPlayerScore == 27);
        }

        [TestMethod]
        public void Cheater_vs_Cooperator_Score_Is_Correct()
        {
            Player firstPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCheater());
            Player secondPlayer = CreatePlayer(NoMemoryStrategies.GetSimpleCooperator());

            Game game = gameService.Play(firstPlayer, secondPlayer);

            int firstPlayerScore = game.Rounds.Sum(r => r.FirstPlayerScore);
            int secondPlayerScore = game.Rounds.Sum(r => r.SecondPlayerScore);

            Assert.IsTrue(firstPlayerScore == 50);
            Assert.AreEqual(0, secondPlayerScore);
        }

        private Player CreatePlayer(Strategy strategy)
        {
            return new Player()
            {
                StrategyName = strategy.Name,
                StrategyId = strategy.Id,
                Strategy = strategy,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}