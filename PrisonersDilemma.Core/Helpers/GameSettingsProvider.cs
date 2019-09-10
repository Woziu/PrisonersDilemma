using System;
using System.Collections.Generic;
using System.Text;
using PrisonersDilemma.Core.Settings;

namespace PrisonersDilemma.Core.Helpers
{
    public class GameSettingsProvider : IGameSettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            //TODO: move to config
            return new GameSettings()
            {
                MoveModifier = -1,                
                CooperateModifier = 3,
                TotalRounds = 10
            };            
        }
    }
}
