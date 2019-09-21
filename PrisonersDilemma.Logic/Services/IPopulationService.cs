using PrisonersDilemma.Core.Models;
using System.Collections.Generic;

namespace PrisonersDilemma.Logic.Services
{
    public interface IPopulationService
    {
        Population Evaluate(List<Player> players);
        Population GetNewPopulation(Population population);
        bool IsPopulationConsistent(Population population);
        Dictionary<string, int> GetScorePerStrategy(Population population);
    }
}
