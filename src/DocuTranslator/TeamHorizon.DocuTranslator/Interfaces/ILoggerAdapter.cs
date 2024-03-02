using System;

namespace TeamHorizon.DocuTranslator.Interfaces
{
    public interface ILoggerAdapter<TType>
    {
        void LogError(Exception exception, string message);
        void LogError(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogCritical(Exception exception, string message);
        void LogCritical(string message);
    }
}
