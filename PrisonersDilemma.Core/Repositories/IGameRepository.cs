using PrisonersDilemma.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Core.Repositories
{
    public interface IGameRepository
    {        
        Task SaveAsync(string populationId, Game game);
        Task SaveAsync(string populationId, List<Game> games);
    }
}
