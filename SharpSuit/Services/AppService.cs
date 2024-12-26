using static SharpSuit.Services.UIService;
using static SharpSuit.Apps.SharpSniffer.Services.UIService;
using static SharpSuit.Apps.SharpXSS.Services.UIService;

namespace SharpSuit.Services
{
    public class AppService
    {
        public static void StartTools()
        {
            string input = string.Empty;
            do
            {
                DisplayMessage( NewLine.Yes, TextColor.None, "Choose tool:");
                DisplayMessage(NewLine.Yes, TextColor.None, "[0] sharpxss - analyze a web application for XSS vulnerabilities");
                DisplayMessage(NewLine.Yes, TextColor.None, "[1] sharpsniffer - network traffic analysing tool");
                DisplayMessage(NewLine.Yes, TextColor.None, "Type -h for help with commands");
                input = Console.ReadLine()!.Replace(" ", "");
                switch (input)
                {
                    case "-h":
                        CommandHelp();
                        break;
                    case "-e":
                        DisplayMessage(NewLine.Yes, TextColor.Error, "Exiting....");
                        break;
                    case "0":
                        StartSharpXSS();
                        break;
                    case "1":
                        StartSharpSniffer(); // fix commands, etc
                        break;
                    default:
                        CommandHelp();
                        break;
                }
                DisplayMessage(NewLine.Yes, TextColor.None);
            }
            while (input != "-e");
        }
    }
}
