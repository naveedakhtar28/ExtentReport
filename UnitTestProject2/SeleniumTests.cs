using System.Threading;

namespace ExtentReportSelenium
{
    public class SeleniumTest : Utilities
    {

        [Test]
        public void TestMethod1()
        {
            driver.Url = "https://www.google.com/gmail/about/#";
            Thread.Sleep(3000);
        }


        [Test]
        public void TestMethod2()
        {
            driver.Url = "https://qaweb.empwr.com/";
            Thread.Sleep(3000);
        }

        [Test]
        public void TestMethod3()
        {
            driver.Url = "https://www.news18.com/";
            Thread.Sleep(3000);
        }

    }
}

