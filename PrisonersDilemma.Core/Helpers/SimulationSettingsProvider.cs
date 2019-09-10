using System;
using System.Collections.Generic;
using System.Text;
using PrisonersDilemma.Core.Settings;

namespace PrisonersDilemma.Core.Helpers
{
    public class SimulationSettingsProvider : ISimulationSettingsProvider
    {
        public SimulationSettings GetSimulationSettings()
        {
            //TODO: Move to config
            return new SimulationSettings() { PoplationsLimit  = 10};
        }
    }
}
