using System;
using System.Collections.Generic;
using System.Text;

namespace PrisonersDilemma.Core.Helpers
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}
