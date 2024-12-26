using Newtonsoft.Json.Linq;
using SharpSuit.Apps.SharpXSS.AppConstants;
using System.Text;
using static SharpSuit.Apps.SharpXSS.AppConstants.PathConstants.PayloadConstants;
using static SharpSuit.Apps.SharpXSS.Services.DriverService;
using static SharpSuit.Apps.SharpXSS.Services.UIService;
using static SharpSuit.Services.UIService;

namespace SharpSuit.Apps.SharpXSS.Services
{
    public class AppService
    {
        public static async Task<string> GetLocationAsync(string ip)
        {
            using (HttpClient client = new HttpClient())
            {
                // Use ipinfo.io for geolocation
                string url = $"https://ipinfo.io/{ip}/json";


                // Make the HTTP GET request
                var response = await client.GetStringAsync(url);

                // Parse the JSON response
                JObject json = JObject.Parse(response);

                StringBuilder sb = new StringBuilder();
                foreach (var item in json)
                {
                    if (item.Key == "ip")
                        sb.Append(item.Value + ", ");
                    if (item.Key == "city")
                        sb.Append(item.Value);
                }
                return sb.ToString();
            }
        }
        public static void SharpXSSLogicAsync()
        {
            try
            {
                string userInput = "";
                do
                {
                    DisplayMessage(NewLine.No, TextColor.None, "-> ");
                    userInput = Console.ReadLine()!;

                    if (userInput.Contains("-h")) // help
                    {
                        PrintControls();
                    }
                    else if (userInput.Contains("-p") && !userInput.Contains("-t") && !userInput.Contains("-s")) // show payload options
                    {

                        if (userInput.Replace(" ", "") == "-p")
                        {
                            List<string> payloadList = PayloadOptions.Payloads.ToList();

                            DisplayMessage(NewLine.Yes, TextColor.Formal, "Printing xss payload options...");
                            foreach (var item in payloadList)
                            {
                                DisplayMessage(NewLine.Yes, TextColor.None, $"{item}");
                            }
                            DisplayMessage(NewLine.Yes, TextColor.Success, "Payload options printed.");
                            continue;
                        }

                        DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid command");
                        PrintControls();
                        continue;
                    }
                    else if (userInput.Contains("-p") && userInput.Contains("-t")) // analyze a target with a payload
                    {
                        var splitted = userInput.Split(new string[] { "-p ", "-t " }, StringSplitOptions.RemoveEmptyEntries);

                        string payloadInput = splitted[0].Replace(" ", "").ToLower();

                        string payloadType = payloadInput != "basic" && payloadInput != "alert" && payloadInput != "body" && payloadInput != "cloudflare"
                                            && payloadInput != "svg" && payloadInput != "waf" && payloadInput != "polyglot" && payloadInput != "img"
                                            && payloadInput != "div" && payloadInput != "custom" && payloadInput != "akamai" && payloadInput != "cloudfront"
                                            && payloadInput != "imperva" && payloadInput != "incapsula" && payloadInput != "wordfence" && payloadInput != "audio"
                                            ? null! : payloadInput;

                        if (payloadType == null)
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid payload");
                            PrintControls();
                            continue;
                        }


                        string targetUrl = splitted[1];

                        if (targetUrl == null)
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid target");
                            PrintControls();
                            continue;
                        }

                        if (RunAsync(payloadType, targetUrl))
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Success, "Scan conducted successfully.");
                            continue;
                        }
                        else
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Error, "Scan discontinued.");
                            continue;
                        }

                    }
                    else if (userInput.Contains("-p") && userInput.Contains("-s")) // show payload contents
                    {
                        var splitted = userInput.Split(new string[] { "-p ", "-s" }, StringSplitOptions.RemoveEmptyEntries);

                        string payloadInput = splitted[0].Replace(" ", "").ToLower();

                        string payloadType = payloadInput != "basic" && payloadInput != "alert" && payloadInput != "body" && payloadInput != "cloudflare"
                                            && payloadInput != "svg" && payloadInput != "waf" && payloadInput != "polyglot" && payloadInput != "img"
                                            && payloadInput != "div" && payloadInput != "custom" && payloadInput != "akamai" && payloadInput != "cloudfront"
                                            && payloadInput != "imperva" && payloadInput != "incapsula" && payloadInput != "wordfence" && payloadInput != "audio"
                                            ? null! : payloadInput;

                        if (payloadType == null)
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid payload");
                            PrintControls();
                            continue;
                        }

                        PrintPayload(payloadType);
                        continue;
                    }
                    else if (userInput.Contains("-e")) // exit
                    {
                        DisplayMessage(NewLine.Yes, TextColor.Error, "Exiting...");
                        break;
                    }
                    else
                    {
                        DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid input.");
                        PrintControls();
                        continue;
                    }

                }
                while (!userInput.Contains("-e"));

            }
            catch (Exception ex)
            {
                DisplayMessage(NewLine.Yes, TextColor.Error, $"{ex.Message}, ABORTING...");
            }
        }
        public static async Task<bool> IsWebsiteOnlineAsync(string targetUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(targetUrl);
                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException ex)
                {
                    DisplayMessage(NewLine.Yes, TextColor.Error, $"{ex.Message}, ABORTING...");
                    return false;
                }
            }
        }
        public static async Task<string> GetLocalIPAddressAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Use api.ipify.org to get the public IP
                string url = "https://api.ipify.org"; // or use https for better security
                string ip = await client.GetStringAsync(url);
                return ip;
            }
        }
        public static bool RunAsync(string payloadType, string targetUrl)
        {
            try
            {
                //if (await IsWebsiteOnlineAsync(targetUrl))
                //{
                //	DisplayMessage($"{targetUrl} is up and running.", NewLine.Yes, TextColor.Success);
                //}

                // check if payloads exist in the Payloads directory 
                switch (payloadType)
                {
                    case "basic":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading basic XSS payloads...");
                        string xssPath = basicXSSPath;
                        List<string> xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Basic XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "audio":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading audio XSS payloads...");
                        xssPath = audioXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Aydio XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "alert":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading alert XSS payloads...");
                        xssPath = alertXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Alert XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "body":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading body tag XSS payloads...");
                        xssPath = bodyXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Body Tag XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "cloudflare":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading cloudflare XSS payloads...");
                        xssPath = cloudflareXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Cloudflare XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "div":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading div tag XSS payloads...");
                        xssPath = divXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Div Tag XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "img":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading image tag XSS payloads...");
                        xssPath = imgXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Image Tag XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "polyglot":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading polyglot XSS payloads...");
                        xssPath = polyglotXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Polyglot XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "svg":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading SVG XSS payloads...");
                        xssPath = svgXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for SVG XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "waf":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading WAF bypassing XSS payloads...");
                        xssPath = wafXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for WAF XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "akamai":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading Akamai bypassing XSS payloads...");
                        xssPath = akamaiXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Akamai XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "cloudfront":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading Cloudfront bypassing XSS payloads...");
                        xssPath = cloudfrontXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Cloudfront XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "imperva":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading Imperva bypassing XSS payloads...");
                        xssPath = impervaXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Imperva XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "incapsula":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading Incapsula bypassing XSS payloads...");
                        xssPath = incapsulaXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Incapsula XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "wordfence":
                        DisplayMessage(NewLine.Yes, TextColor.Formal, "Loading Wordfence bypassing XSS payloads...");
                        xssPath = wordfenceXSSPath;
                        xssPayloads = File.ReadAllLines(@xssPath).ToList();
                        DisplayMessage(NewLine.Yes, TextColor.Success, "XSS payloads loaded!");

                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for Wordfence XSS vulnerablities.");
                        foreach (var payload in xssPayloads)
                        {
                            CheckInBrowser(targetUrl, payload);
                        }
                        return true;
                    case "custom":
                        DisplayMessage(NewLine.No, TextColor.Formal, "Enter your custom XSS payload: "  );
                        string customPayload = Console.ReadLine()!;
                        if (customPayload == null)
                        {
                            DisplayMessage(NewLine.Yes, TextColor.Error, "Payload cannot be empty, ABORTING...");
                            break;
                        }
                        DisplayMessage(NewLine.Yes, TextColor.Formal, $"Analyzing {targetUrl} for custom XSS vulnerablities.");
                        CheckInBrowser(targetUrl, customPayload);
                        return true;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(NewLine.Yes, TextColor.Error, $"{ex.Message}, ABORTING...");
            }

            return false;
        }
    }
}