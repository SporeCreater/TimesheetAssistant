using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace PageDrivers.Tests
{
    [TestClass]
    public abstract class WatinDriverTestSuite
    {
        protected IE _ie;

        [TestInitialize]
        public void Setup()
        {
            _ie = new IE(Config.APP_URL);
        }

        [TestCleanup]
        public void Teardown()
        {
            _ie.Dispose();
        }
    }
}