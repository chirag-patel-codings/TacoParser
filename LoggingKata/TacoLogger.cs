using System;

namespace LoggingKata
{
    // Displays errors in the Console by implementing members of 'ILog' interface
    public class TacoLogger : ILog
    {
        private ConsoleColor _consoleForegroundColor;
        
        public TacoLogger()
        {
            _consoleForegroundColor = Console.ForegroundColor;
        }
        public void LogFatal(string log, Exception exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFatal: {log}, Exception {exception}");
            Console.ForegroundColor = _consoleForegroundColor;
        }

        public void LogError(string log, Exception exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {log}, Exception {exception}");
            Console.ForegroundColor = _consoleForegroundColor;
        }

        public void LogWarning(string log)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nWarning: {log}");
            Console.ForegroundColor = _consoleForegroundColor;
        }

        public void LogInfo(string log)
        {
            Console.WriteLine($"Info: {log}");
        }

        public void LogDebug(string log)
        {
            Console.WriteLine($"\nDebug: {log}");
        }
    }
}
