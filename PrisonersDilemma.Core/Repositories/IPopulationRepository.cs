using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface IPopulationRepository
    {
        Task SavePopulationAsync(string simulationId, Population population);
    }
}
