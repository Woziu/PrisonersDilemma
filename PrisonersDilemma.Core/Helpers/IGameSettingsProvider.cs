using PrisonersDilemma.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Helpers
{
    public interface IGameSettingsProvider
    {
        GameSettings GetGameSettings();
    }
}
