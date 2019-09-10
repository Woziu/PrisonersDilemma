using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Tests.Integration.Strategies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration
{
    [TestClass]
    public class StrategyRepositoryTests
    {
        IConnectionStringProvider connection;
        [TestInitialize]
        public void GetConnectionString()
        {
            if (connection == null)
            {
                MongoTestConventions.RegisterConventions();
                connection = new TestConnectionPrivider("connection.txt");
            }
        }
        [TestMethod]
        public async Task Get_Null_When_No_Strategy()
        {
            var strategyRepository = new StrategyRepository(connection);
            Strategy strategy = await strategyRepository.GetAsync((string)null);
            Assert.IsNull(strategy);
        }

        [TestMethod]
        public async Task Get_Added_Strategy_Name()
        {
            var strategyRepository = new StrategyRepository(connection);
            Strategy strategy = new Strategy()
            {
                Name = "CooperateTest",
                Moves = new List<Move>()
                {
                    new Move() { MoveType = Core.Enums.MoveType.Cooperate}
                }
            };

            string id = await strategyRepository.AddAsync(strategy);
            Strategy strategyFromDb = await strategyRepository.GetAsync(id);
            Assert.AreEqual(strategy.Name, strategyFromDb.Name);
        }

        [TestMethod]
        public async Task PopulateDb()
        {
            //TODO: remove when GUI created
            var strategyRepository = new StrategyRepository(connection);

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
