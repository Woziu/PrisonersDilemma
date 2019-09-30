using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.Tests.Integration.Common;
using PrisonersDilemma.Tests.Integration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.ServicesTests
{
    [TestClass]
    public class StrategyServiceTests
    {
        private IStrategyService strategyService;
        private IStrategyRepository strategyRepository;

        [TestInitialize]
        public void Init()
        {
            MongoTestConventions.RegisterConventions();
            if (strategyService == null)
            {
                strategyService = TestContainer.BuildContainer().Resolve<IStrategyService>();
            }
            if (strategyRepository == null)
            {
                strategyRepository = TestContainer.BuildContainer().Resolve<IStrategyRepository>();
            }
        }

        [TestMethod]
        public async Task Get_Strategies_By_Id_Count_Equal_All_Strategies()
        {
            var strategiesIds = strategyService.GetAllStrategies().Result.Select(s => s.Id).ToList();
            var strategies = await strategyService.GetStrategiesById(strategiesIds);
            Assert.AreEqual(strategiesIds.Count, strategies.Count);
        }

        [TestMethod]
        public async Task Get_Strategies_By_Id_Count_Equal_Distinct_Strategies()
        {
            List<Strategy> allStrategies = await strategyService.GetAllStrategies();
            Strategy cheater = allStrategies.Where(s => s.Name == "Simple Cheater").FirstOrDefault();
            Strategy cooperator = allStrategies.Where(s => s.Name == "Simple Cooperator").FirstOrDefault();

            List<Player> players = new List<Player>();
            for (int i = 0; i < 5; i++)
            {
                players.Add(new Player() { StrategyId = cheater.Id });
                players.Add(new Player() { StrategyId = cooperator.Id });
            }

            var strategies = await strategyService.GetStrategiesById(players.Select(p => p.StrategyId).ToList());
            Assert.AreEqual(2, strategies.Count);
        }


        [TestMethod]
        public void GoodStrategy_Cheat_In_First_Move()
        {
            Player goodPlayer = GetGoodPlayer();

            PlayerMove move = strategyService.GetNextMove(goodPlayer, new List<Round>());

            Assert.AreEqual(MoveType.Cheat, move.Type);
        }
        [TestMethod]
        public void GoodStrategy_Cheat_In_Second_Move()
        {
            Player goodPlayer = GetGoodPlayer();
            var rounds = new List<Round>()
            {
                new Round()
                {
                    Id = 1,
                    PlayersMoves = new List<PlayerMove>()
                    {
                        new PlayerMove() { PlayerId = goodPlayer.Id, Type = MoveType.Cheat },
                        new PlayerMove() { PlayerId = "other player", Type = MoveType.Cooperate }
                    }
                }
            };

            PlayerMove move = strategyService.GetNextMove(goodPlayer, rounds);

            Assert.AreEqual(MoveType.Cheat, move.Type);
        }
        [TestMethod]
        public void GoodStrategy_Cooperate_In_Third_Move()
        {
            Player goodPlayer = GetGoodPlayer();
            var rounds = new List<Round>()
            {
                new Round()
                {
                    Id = 1,
                    PlayersMoves = new List<PlayerMove>()
                    {
                        new PlayerMove() { PlayerId = goodPlayer.Id, Type = MoveType.Cheat },
                        new PlayerMove() { PlayerId = "other player", Type = MoveType.Cooperate }
                    }
                },
                new Round()
                {
                    Id = 2,
                    PlayersMoves = new List<PlayerMove>()
                    {
                        new PlayerMove() { PlayerId = goodPlayer.Id, Type = MoveType.Cheat },
                        new PlayerMove() { PlayerId = "other player", Type = MoveType.Cooperate }
                    }
                }
            };

            PlayerMove move = strategyService.GetNextMove(goodPlayer, rounds);

            Assert.AreEqual(MoveType.Cooperate, move.Type);
        }

        private Player GetGoodPlayer()
        {
            Strategy goodStrategy = TwoRoundStrategies.GetGoodStrategy();
            return new Player()
            {
                StrategyName = goodStrategy.Name,
                StrategyId = goodStrategy.Id,
                Strategy = goodStrategy,
                Id = Guid.NewGuid().ToString()
            };

        }
    }
}
