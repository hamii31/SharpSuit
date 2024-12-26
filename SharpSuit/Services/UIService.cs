using static SharpSuit.Services.AppService;

namespace SharpSuit.Services
{
    public class UIService
    {
        public static void StartSharpSuit()
        {
            DisplayMessage(NewLine.Yes, TextColor.None, " _______           _______  _______  _______  _______          __________________\r\n(  ____ \\|\\     /|(  ___  )(  ____ )(  ____ )(  ____ \\|\\     /|\\__   __/\\__   __/\r\n| (    \\/| )   ( || (   ) || (    )|| (    )|| (    \\/| )   ( |   ) (      ) (   \r\n| (_____ | (___) || (___) || (____)|| (____)|| (_____ | |   | |   | |      | |   \r\n(_____  )|  ___  ||  ___  ||     __)|  _____)(_____  )| |   | |   | |      | |   \r\n      ) || (   ) || (   ) || (\\ (   | (            ) || |   | |   | |      | |   \r\n/\\____) || )   ( || )   ( || ) \\ \\__| )      /\\____) || (___) |___) (___   | |   \r\n\\_______)|/     \\||/     \\||/   \\__/|/       \\_______)(_______)\\_______/   )_(   \r\n                                                                                 ");

            StartTools();
        }

        public static void CommandHelp()
        {
            DisplayMessage(NewLine.Yes, TextColor.None, "Command Help:");
            DisplayMessage(NewLine.Yes, TextColor.None, "Type the [id] of the tool to start the tool");
            DisplayMessage(NewLine.Yes, TextColor.Error, "-e exit");
        }

        public static void DisplayMessage(NewLine newLine, TextColor severity, string message = null!)
        {

            switch (severity)
            {
                case TextColor.Formal:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case TextColor.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case TextColor.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TextColor.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case TextColor.None:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            switch (newLine)
            {
                case NewLine.Yes:
                    Console.WriteLine(message);
                    Console.ResetColor(); // Reset to default color
                    break;
                case NewLine.No:
                    Console.Write(message);
                    Console.ResetColor(); // Reset to default color
                    break;
            }
        }
        public enum NewLine
        {
            Yes = 0,
            No = 1
        }
        public enum TextColor
        {
            Formal = 0,
            Success = 1,
            Warning = 2,
            Error = 3,
            None = 4
        }
    }
}
