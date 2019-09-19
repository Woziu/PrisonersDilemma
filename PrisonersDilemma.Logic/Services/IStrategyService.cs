using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface IStrategyService
    {
        PlayerMove GetNextMove(Player player, List<Round> roundsHistory);
        Task<List<Strategy>> GetStrategiesById(List<string> idList);
        Task<List<Strategy>> GetAllStrategies();
        Task<Strategy> GetStrategyByName(string name);
    }
}
