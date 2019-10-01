using Autofac;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;

namespace PrisonersDilemma.Tests.Integration.Common
{
    public class TestContainer
    {
        private static IContainer _container;

        public static IContainer BuildContainer()
        {
            if (_container == null)
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<TestConnectionPrivider>()
                    .As<IConnectionStringProvider>()
                    .WithParameter(new TypedParameter(typeof(string), "connection.txt"));

                builder.RegisterType<SimulationRepository>().As<ISimulationRepository>();
                builder.RegisterType<StrategyRepository>().As<IStrategyRepository>();

                builder.RegisterType<TestGameSettingsProvider>().As<IGameSettingsProvider>();
                builder.RegisterType<SimulationSettingsProvider>().As<ISimulationSettingsProvider>();

                builder.RegisterType<StrategyService>().As<IStrategyService>();
                builder.RegisterType<GameService>().As<IGameService>();
                builder.RegisterType<PopulationService>().As<IPopulationService>();
                builder.RegisterType<SimulationService>().As<ISimulationService>();

                _container = builder.Build();
            }
            return _container;
        }
    }
}
