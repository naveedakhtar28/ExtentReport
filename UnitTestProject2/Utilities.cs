using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Threading;

namespace ExtentReportSelenium
{
    public class Utilities
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var ExecutionBrowser = System.Environment.GetEnvironmentVariable("Browser");
            var ExecutionEnvironment = System.Environment.GetEnvironmentVariable("Environment");

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
        }
           
        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

    }
}

