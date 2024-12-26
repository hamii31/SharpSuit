using static SharpSuit.Apps.SharpSniffer.Services.AppService;

namespace SharpSuit.Apps.SharpSniffer.Services
{
    public class UIService
    {
        public static void StartSharpSniffer()
        {
            Console.WriteLine("      _                                _  __  __          \r\n     | |                              (_)/ _|/ _|         \r\n  ___| |__   __ _ _ __ _ __  ___ _ __  _| |_| |_ ___ _ __ \r\n / __| '_ \\ / _` | '__| '_ \\/ __| '_ \\| |  _|  _/ _ \\ '__|\r\n \\__ \\ | | | (_| | |  | |_) \\__ \\ | | | | | | ||  __/ |   \r\n |___/_| |_|\\__,_|_|  | .__/|___/_| |_|_|_| |_| \\___|_|   \r\n                      | |                                 \r\n                      |_|                                 ");
            Console.WriteLine("Capturing network devices...");
            Thread.Sleep(1000);
            CaptureDevices();
        }
    }
}
