using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Tests.Integration.Common;
using PrisonersDilemma.Tests.Integration.Strategies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.RepositoriesTests
{
    [TestClass]
    public class StrategyRepositoryTests
    {
        [TestInitialize]
        public void Init()
        {
            MongoTestConventions.RegisterConventions();
        }

        [TestMethod]
        public async Task Get_Null_When_No_Strategy()
        {
            var strategyRepository = TestContainer.BuildContainer().Resolve<IStrategyRepository>();
            Strategy strategy = await strategyRepository.GetAsync((string)null);
            Assert.IsNull(strategy);
        }

        [TestMethod]
        public async Task Get_Added_Strategy_Name()
        {
            var strategyRepository = TestContainer.BuildContainer().Resolve<IStrategyRepository>();
            Strategy strategy = new Strategy()
            {
                Name = "CooperateTest",
                Moves = new List<Move>() { new Move() { MoveType = Core.Enums.MoveType.Cooperate } }
            };

            string id = await strategyRepository.AddAsync(strategy);
            Strategy strategyFromDb = await strategyRepository.GetAsync(id);
            Assert.AreEqual(strategy.Name, strategyFromDb.Name);
        }

        [TestMethod]
        public async Task PopulateDb()
        {
            //TODO: remove when GUI created
            var strategyRepository = TestContainer.BuildContainer().Resolve<IStrategyRepository>();

            var strategies = new List<Strategy>()
            {
                NoMemoryStrategies.GetSimpleCheater(),
                NoMemoryStrategies.GetSimpleCooperator(),
                OneRoundStrategies.GetCopycat(),
                OneRoundStrategies.GetCheaterCopycat(),
                OneRoundStrategies.GetReverser(),
                OneRoundStrategies.GetCheaterReverser()
            };

            var newStrategies = new List<string>();
            foreach (Strategy strategy in strategies)
            {
                Strategy tmp = await strategyRepository.GetByNameAsync(strategy.Name);
                if (tmp == null)
                {
                    string newId = await strategyRepository.AddAsync(strategy);
                    newStrategies.Add(newId);
                }
            }

            List<Strategy> addedStrategies = await strategyRepository.GetAsync(newStrategies);

            Assert.AreEqual(newStrategies.Count, addedStrategies.Count);
        }

    }
}
