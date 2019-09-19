﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Tests.Integration.RepositoriesTests
{
    [TestClass]
    public class SimulationRepositoryTests
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
        public async Task Get_New_Simulation_Id()
        {
            var simulationRepository = new SimulationRepository(connection);
            var simulation = new Simulation()
            {
                StartDate = DateTime.Now,
                PopulationsLimit = 10,
                EntryPlayers = new List<Player>(),
                Populations = new List<Population>()
            };
            string id = await simulationRepository.SaveAsync(simulation);
            Assert.IsFalse(String.IsNullOrEmpty(id));
        }

        [TestMethod]
        public async Task Update_Simulation()
        {
            var simulationRepository = new SimulationRepository(connection);
            var simulation = new Simulation()
            {
                StartDate = DateTime.Now,
                PopulationsLimit = 10,
                EntryPlayers = new List<Player>(),
                Populations = new List<Population>()
            };
            string id = await simulationRepository.SaveAsync(simulation);
            simulation.FinishDate = DateTime.Now;
            simulation.PopulationsCompleated = 10;
            await simulationRepository.UpdateAsync(simulation);

            Simulation updatedSimulation = await simulationRepository.GetAsync(id);

            Assert.IsFalse(String.IsNullOrEmpty(updatedSimulation.FinishDate.ToString()));
        }

    }
}