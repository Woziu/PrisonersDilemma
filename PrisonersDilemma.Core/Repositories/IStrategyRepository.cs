using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface IStrategyRepository
    {
        Strategy Get(string id);
        List<Strategy> Get(List<string> idList);
        Task<List<Strategy>> GetAll();
        Task<Strategy> GetAsync(string id);
        Task<Strategy> GetByNameAsync(string strategyName);
        string Add(Strategy strategy);
        Task<string> AddAsync(Strategy strategy);
    }
}
