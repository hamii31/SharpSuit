using static SharpSuit.Apps.SharpXSS.AppConstants.PathConstants.PayloadConstants;
using static SharpSuit.Apps.SharpXSS.Services.AppService;
using static SharpSuit.Services.UIService;

namespace SharpSuit.Apps.SharpXSS.Services
{
    public class UIService
    {
        public static void StartSharpXSS()
        {
            DisplayMessage(NewLine.Yes, TextColor.None,
                    "       .__                                              \r\n" +
                    "  _____|  |__ _____ ________________  ___  ______ ______\r\n" +
                    " /  ___/  |  \\\\__  \\\\_  __ \\____ \\  \\/  / /  ___//  ___/\r\n" +
                    " \\___ \\|   Y  \\/ __ \\|  | \\/  |_> >    <  \\___ \\ \\___ \\ \r\n" +
                    "/____  >___|  (____  /__|  |   __/__/\\_ \\/____  >____  >\r\n" +
                    "     \\/     \\/     \\/      |__|        \\/     \\/     \\/ ");

            DisplayMessage(NewLine.Yes, TextColor.None, "\n-> -h for usage guide and options");

            SharpXSSLogicAsync();
        }
        public static void PrintControls()
        {
            DisplayMessage(NewLine.Yes, TextColor.None, $"{Environment.NewLine}-> Usage: ");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -p <payload> -t <target>");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -p <payload> -s");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -h");
            DisplayMessage(NewLine.Yes, TextColor.None, $"-> -e {Environment.NewLine}");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> Information: ");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -p payload type (-p audio, basic, alert, body, cloudflare, div, img, polyglot, svg, waf, custom)");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -t target URL (-t ex.: https://demo.owasp-juice.shop/#/search?q=)");
            DisplayMessage(NewLine.Yes, TextColor.None, "-> -s show payloads in a specific list (-p basic -s)");
            DisplayMessage(NewLine.Yes, TextColor.None, $"-> -e exit{Environment.NewLine}");
        }
        public static void PrintPayload(string payload)
        {
            string payloadPath = payload.Equals("basic") ? basicXSSPath
                                : payload.Equals("alert") ? alertXSSPath
                                : payload.Equals("body") ? bodyXSSPath
                                : payload.Equals("cloudflare") ? cloudflareXSSPath
                                : payload.Equals("div") ? divXSSPath
                                : payload.Equals("img") ? imgXSSPath
                                : payload.Equals("polyglot") ? polyglotXSSPath
                                : payload.Equals("svg") ? svgXSSPath
                                : payload.Equals("waf") ? wafXSSPath
                                : payload.Equals("audio") ? audioXSSPath
                                : null!;

            if (payloadPath != null)
            {
                try
                {
                    DisplayMessage(NewLine.Yes, TextColor.Formal, "Printing payload...");
                    Thread.Sleep(3000);
                    DisplayMessage(NewLine.Yes, TextColor.None, string.Join(Environment.NewLine, File.ReadAllLines(payloadPath)));
                    DisplayMessage(NewLine.Yes, TextColor.Success, "Payload printed.");
                }
                catch (Exception ex)
                {
                    DisplayMessage(NewLine.Yes, TextColor.Error, ex.Message);
                }
            }
            else
            {
                DisplayMessage(NewLine.Yes, TextColor.Error, "Invalid payload!");
            }
        }
       
    }
}
