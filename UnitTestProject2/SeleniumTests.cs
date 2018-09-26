using NUnit.Framework;
using System.Threading;

namespace ExtentReportSelenium
{
    public class SeleniumTest : Utilities
    {

        [Test]
        public void TestMethod1()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(2000);
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod3()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod4()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod5()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestMethod6()
        {
            Assert.IsTrue(false);
        }

    }
}

