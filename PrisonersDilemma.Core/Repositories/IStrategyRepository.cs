using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface IStrategyRepository
    {
        Strategy GetStrategyById(string id);
        Task<Strategy> GetStrategyByIdAsync(string id);
    }
}
