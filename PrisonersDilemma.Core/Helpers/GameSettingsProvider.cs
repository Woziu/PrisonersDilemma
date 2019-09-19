using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using PrisonersDilemma.Core.Settings;

namespace PrisonersDilemma.Core.Helpers
{
    public class GameSettingsProvider : IGameSettingsProvider
    {
        public GameSettings GetGameSettings()
        {
            var configKeys = new List<string>() { "MoveModifier", "CooperateModifier", "TotalRounds" };
            var configValues = new Dictionary<string, int>();

            foreach(string key in configKeys)
            {
                try
                {
                    configValues[key] = Convert.ToInt32(ConfigurationManager.AppSettings[key]);
                }
                catch(Exception e)
                {
                    throw new FormatException($"Couldnt parse {key} value", e);
                }
            }

            return new GameSettings()
            {
                MoveModifier = configValues["MoveModifier"],
                CooperateModifier = configValues["CooperateModifier"],
                TotalRounds = configValues["TotalRounds"],
            };            
        }
    }
}
