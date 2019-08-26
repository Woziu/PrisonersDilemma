using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;

namespace PrisonersDilemma.Logic.Services
{
    public class StrategyService : IStrategyService//TODO: Change to PlayerService - get strategy in ctor, GetNextMove as method
    {        
        public Task<PlayerMove> GetNextMoveAsync(Player player, List<Round> roundsHistory)
        {
            throw new NotImplementedException();
        }
    }
}
