using PrisonersDilemma.Core.Models;

namespace PrisonersDilemma.Logic.Services
{
    public interface IGameService
    {
        Game Play(Player firstPlayer, Player secondPlayer);
    }
}
