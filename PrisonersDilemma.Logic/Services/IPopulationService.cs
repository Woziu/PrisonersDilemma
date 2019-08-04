using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface IPopulationService
    {
        Task Evaluate();
        Task<Population> GetNewPopulation();
        Task<bool> IsConsistent();
    }
}
