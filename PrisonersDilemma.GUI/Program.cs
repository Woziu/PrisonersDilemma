using Autofac;
using MongoDB.Bson.Serialization.Conventions;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Logic.Services;
using System;
using System.Windows.Forms;

namespace PrisonersDilemma.GUI
{
    static class Program
    {
        private static IContainer container;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConventionRegistry.Register("CustomConventions", new MongoConventions(), x => true);
            container = BuildContainer();

            Application.Run(container.Resolve<Form1>());
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();

            builder.RegisterType<SimulationRepository>().As<ISimulationRepository>();
            builder.RegisterType<StrategyRepository>().As<IStrategyRepository>();

            builder.RegisterType<GameSettingsProvider>().As<IGameSettingsProvider>();
            builder.RegisterType<SimulationSettingsProvider>().As<ISimulationSettingsProvider>();

            builder.RegisterType<StrategyService>().As<IStrategyService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<PopulationService>().As<IPopulationService>();
            builder.RegisterType<SimulationService>().As<ISimulationService>();

            builder.RegisterType<Form1>().UsingConstructor(typeof(ISimulationService), typeof(IStrategyService));

            return builder.Build();
        }
    }
}
