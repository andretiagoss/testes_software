using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace NerdStore.BDD.Tests.Config
{
    //Factory que vai criar o WebDriver que o Selenium vai utilizar. 
    //Representa o Browser.
    public static class WebDriverFactory
    {
        //O parametro deadless define se o browser será ou não aberto
        public static IWebDriver CreateWebDriver(Browser browser, string caminhoDriver, bool headless)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    var optionsFireFox = new FirefoxOptions();
                    optionsFireFox.AcceptInsecureCertificates = false;
                    if (headless)
                        optionsFireFox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(caminhoDriver, optionsFireFox);

                    break;
                case Browser.Chrome:
                    var options = new ChromeOptions();
                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, options);

                    break;
            }

            return webDriver;
        }
    }
}

