using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface IPopulationService
    {
        Task<Population> Evaluate(List<Player> players);
        Task<Population> GetNewPopulation(Population population);
        Task<bool> IsPopulationConsistent(Population population);
        //Task SavePopulationAsync(string simulationId, Population population);
    }
}
