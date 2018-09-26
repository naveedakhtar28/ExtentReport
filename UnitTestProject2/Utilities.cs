using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ExtentReportSelenium
{
    public class Utilities
    {
        public IWebDriver driver;
        protected ExtentReports _extent;
        protected ExtentTest _test;

        [OneTimeSetUp]
        public void Setup()
        {
            var ExecutionBrowser = System.Environment.GetEnvironmentVariable("Browser");
            var ExecutionEnvironment = System.Environment.GetEnvironmentVariable("Environment");
            var ExecutionTime = System.Environment.GetEnvironmentVariable("BUILD_TIMESTAMP");
            var ExecutionDate = System.Environment.GetEnvironmentVariable("BUILD_DATE");

            switch (ExecutionBrowser)
            {
                case "Chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("start-maximized");
                    options.AddArguments("--incognito");
                    driver = new ChromeDriver("C:/chromedriver_win32/", options);
                    break;

                case "Firefox":
                    //System.Environment.SetEnvironmentVariable("webdriver.gecko.driver", "geckodriver.exe");
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(3);
                    break;

                case "Internet Explorer":
                    driver = new InternetExplorerDriver("C:/IEDriverServer/");
                    break;
            }

            switch (ExecutionEnvironment)
            {
                case "Dev":
                    driver.Url = "https://www.cricbuzz.com/";
                    Thread.Sleep(10000);
                    break;

                case "QA":
                    driver.Url = "http://www.espncricinfo.com/";
                    Thread.Sleep(10000);
                    break;

                case "Prod":
                    driver.Url = "https://www.news18.com/cricketnext/?ref=topnav";
                    Thread.Sleep(10000);
                    break;
            }
            if (ExecutionTime != null)
            {
                var FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),@"Reports");
                if (!Directory.Exists(FilePath + "/" + ExecutionDate))
                {
                    Directory.CreateDirectory(FilePath + "/" + ExecutionDate);
                }
                var fileName = ExecutionTime + ".html";
                var fileDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),@"Reports");
                var htmlReporter = new ExtentHtmlReporter(fileDirectory + "/" + ExecutionDate + "/" + fileName);
                
                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
            }
        }

        [SetUp]
        public void TestSetup()
        {
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            var ExecutionTime = System.Environment.GetEnvironmentVariable("BUILD_TIMESTAMP");
            var ExecutionDate = System.Environment.GetEnvironmentVariable("BUILD_DATE");
            var fileDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Reports");
            var fileNameMethod = this.GetType().ToString() ;
            var fileName = ExecutionTime + "_" +fileNameMethod + ".PNG";

            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(fileDirectory + "\\" + ExecutionDate + "\\" + fileName , ScreenshotImageFormat.Png);
                _test.Fail("details").AddScreenCaptureFromPath(fileName);
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _extent.Flush();
        }
        
        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
            _extent.Flush();
        }

    }
}

