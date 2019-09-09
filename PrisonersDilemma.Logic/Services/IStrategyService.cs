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
        Task<PlayerMove> GetNextMoveAsync(Player player, List<Round> roundsHistory);
        List<Strategy> GetStrategiesById(List<string> idList);
    }
}
