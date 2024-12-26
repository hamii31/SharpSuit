using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using static SharpSuit.Services.UIService;
using static SharpSuit.Apps.SharpXSS.AppConstants.PathConstants.FirefoxConstants;
using static SharpSuit.Apps.SharpXSS.AppConstants.PathConstants.OutputConstants;

namespace SharpSuit.Apps.SharpXSS.Services
{
    public class DriverService
    {
        public static void CheckInBrowser(string url, string payload)
        {
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(firefoxDriverPath);
            var options = new FirefoxOptions();
            options.BinaryLocation = firefoxBinaryLocationPath;

            using (var driver = new FirefoxDriver(firefoxDriverService, options))
            {
                string testUrl = $"{url}{Uri.EscapeDataString(payload)}";

                driver.Navigate().GoToUrl(testUrl);

                Thread.Sleep(2000); // Adjust as necessary

                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // how long to wait before closing window

                    IAlert alert = wait.Until(driver =>
                    {
                        try
                        {
                            return driver.SwitchTo().Alert();
                        }
                        catch (NoAlertPresentException)
                        {
                            return null!; // Continue waiting
                        }
                    });

                    if (alert != null)
                    {
                        DisplayMessage(NewLine.Yes, TextColor.Success, $"{alert.Text} detected from payload: " + payload);

                        string outputPath = outputXSSPath; //writes all working XSS payloads to the output_xss
                        using (StreamWriter writer = new StreamWriter(outputPath))
                        {
                            writer.WriteLine(payload);
                        }

                        alert.Accept(); // Dismiss the alert
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    DisplayMessage(NewLine.Yes, TextColor.Warning, $"No alert detected, {payload} did not execute.");
                }
            }
        }
    }
}
