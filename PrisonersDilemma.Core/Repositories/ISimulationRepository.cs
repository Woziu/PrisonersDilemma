using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface ISimulationRepository
    {
        Task<Simulation> GetAsync(string id);
        Task<string> SaveAsync(Simulation simulation);
        Task UpdateAsync(Simulation simulation);
    }
}
