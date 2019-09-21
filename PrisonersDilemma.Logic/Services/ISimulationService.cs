using PrisonersDilemma.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonersDilemma.Logic.Services
{
    public interface ISimulationService
    {
        Task<Simulation> Run(List<Player> players);
        Task<List<Player>> GetPlayersStrategies(List<Player> players);
    }
}
