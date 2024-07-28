namespace TestGutenberg;

using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;


    public class BaseSetup
    {
        public static IWebDriver? driver;

        private static string? browserName = ConfigurationManager.AppSettings["Browser"];
        private static string? startURL = ConfigurationManager.AppSettings["StartURL"];

        [SetUp]
        public void Setup()
        {
            if (browserName.Equals("Chrome"))
            {
                var deviceDriver = ChromeDriverService.CreateDefaultService();
                deviceDriver.HideCommandPromptWindow = true;
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-infobars");
                driver = new ChromeDriver(deviceDriver, options);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(startURL);
            }
            else if (browserName.Equals("Firefox"))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
            }
            else if (browserName.Equals("IE"))
            {
                new DriverManager().SetUpDriver(new InternetExplorerConfig());
                driver = new InternetExplorerDriver();
            }
        }   

        [TearDown]
        public void Teardown()
        {
            driver.Dispose();
            driver.Quit();
        }
    }
