using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.ServicesTests
{
    [TestClass]
    public class StrategyServiceTests
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
        public async Task Get_Strategies_By_Id_Count_Equal_All_Strategies()
        {
            var strategyRepository = new StrategyRepository(connection);
            var strategyService = new StrategyService(strategyRepository);
            var strategiesIds = strategyService.GetAllStrategies().Result.Select(s => s.Id).ToList();
            var strategies = await strategyService.GetStrategiesById(strategiesIds);
            Assert.AreEqual(strategiesIds.Count, strategies.Count);
        }

        [TestMethod]
        public async Task Get_Strategies_By_Id_Count_Equal_Distinct_Strategies()
        {
            var strategyRepository = new StrategyRepository(connection);
            var strategyService = new StrategyService(strategyRepository);

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
    }
}
