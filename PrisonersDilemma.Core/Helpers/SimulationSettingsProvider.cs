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
            var configKeys = new List<string>() { "PoplationsLimit", "MutationChancePercent" };
            var configValues = new Dictionary<string, int>();

            foreach (string key in configKeys)
            {
                try
                {
                    configValues[key] = Convert.ToInt32(ConfigurationManager.AppSettings[key]);
                }
                catch (Exception e)
                {
                    throw new FormatException($"Couldnt parse config {key} value", e);
                }
            }

            return new SimulationSettings()
            {
                PoplationsLimit = configValues["PoplationsLimit"],
                MutationChancePercent = configValues["MutationChancePercent"]
            };
        }
    }
}
