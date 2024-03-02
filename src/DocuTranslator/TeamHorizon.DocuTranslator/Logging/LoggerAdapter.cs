using Microsoft.Extensions.Logging;
using System;
using TeamHorizon.DocuTranslator.Interfaces;

namespace TeamHorizon.DocuTranslator.Logging
{
    public class LoggerAdapter<TType> : ILoggerAdapter<TType>
    {
        private readonly ILogger<TType> _logger;

        public LoggerAdapter(ILogger<TType> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception, string message)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(exception, message);
            }
        }

        public void LogError(string message)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(message);
            }
        }

        public void LogInformation(string message)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message);
            }
        }

        public void LogWarning(string message)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogWarning(message);
            }
        }

        public void LogCritical(string message)
        {
            if (_logger.IsEnabled(LogLevel.Critical))
            {
                _logger.LogCritical(message);
            }
        }

        public void LogCritical(Exception exception, string message)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogCritical(exception, message);
            }
        }
    }
}
