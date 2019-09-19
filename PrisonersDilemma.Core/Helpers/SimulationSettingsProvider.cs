using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using PrisonersDilemma.Core.Settings;

namespace PrisonersDilemma.Core.Helpers
{
    public class SimulationSettingsProvider : ISimulationSettingsProvider
    {
        public SimulationSettings GetSimulationSettings()
        {
            string configKey = "PoplationsLimit";
            int poplationsLimit = 10;
            try
            {
                poplationsLimit = Convert.ToInt32(ConfigurationManager.AppSettings[configKey]);
            }
            catch (Exception e)
            {
                throw new FormatException($"Couldnt parse {configKey} value", e);
            }
            return new SimulationSettings() { PoplationsLimit = poplationsLimit };

        }
    }
}
