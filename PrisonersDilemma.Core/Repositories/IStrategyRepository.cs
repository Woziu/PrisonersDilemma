using PrisonersDilemma.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface IStrategyRepository
    {
        Task<List<Strategy>> GetAsync(List<string> idList);
        Task<List<Strategy>> GetAll();
        Task<Strategy> GetAsync(string id);
        Task<Strategy> GetByNameAsync(string strategyName);
        Task<string> AddAsync(Strategy strategy);
    }
}
