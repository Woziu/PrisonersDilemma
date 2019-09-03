using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface ISimulationRepository
    {
        void SaveSimulation(Simulation simulation);
        Task SaveSimulationAsync(Simulation simulation);
    }
}
