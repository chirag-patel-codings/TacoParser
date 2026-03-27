using System;
namespace LoggingKata
{

    // Interface (Contract) for logging implementation
    public interface ILog
    {
        // Stubbed-out Function(Members) - same as 'Abstract' members in the 'Abstract' class.
        void LogFatal(string log, Exception exception = null);
        void LogError(string log, Exception exception = null);
        void LogWarning(string log);
        void LogInfo(string log);
        void LogDebug(string log);
    }
}
