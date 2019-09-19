using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrisonersDilemma.Core.Helpers;

namespace PrisonersDilemma.Tests.Integration
{
    [TestClass]
    public class CommonTests
    {        
        [TestMethod]
        public void Get_Mongo_ConnectionString_From_File()
        {
            IConnectionStringProvider connection = new TestConnectionPrivider("connection.txt");
            Assert.IsTrue(connection.GetConnectionString().Contains("mongo"));
        }
    }
}
