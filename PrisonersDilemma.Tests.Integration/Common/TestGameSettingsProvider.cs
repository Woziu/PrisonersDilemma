using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Settings;

namespace PrisonersDilemma.Tests.Integration.Common
{
    public class TestGameSettingsProvider : IGameSettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            return new GameSettings() { TotalRounds = 10 };
        }
    }
}
