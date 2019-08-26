using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface IGameService
    {
        Task SaveGameAsync(string populationId);
        Task<Game> PlayAsync(Player firstPlayer, Player secondPlayer, int totalRounds);
    }
}
