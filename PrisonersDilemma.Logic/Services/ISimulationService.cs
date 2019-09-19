using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface ISimulationService
    {
        Task<Simulation> Run(List<Player> players);
        Task<List<Player>> GetPlayersStrategies(List<Player> players);
    }
}
