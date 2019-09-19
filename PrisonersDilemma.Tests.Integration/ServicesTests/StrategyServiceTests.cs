using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
using PrisonersDilemma.Tests.Integration.Common;
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
    }
}
