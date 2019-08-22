using System;
using System.Threading.Tasks;
using static SimpleLoggingClient.LoggingInterfaces.Enums.Enums;

namespace SimpleLoggingClient.LoggingInterfaces.Logic
{
    public interface IMessageQueue
    {
        void PopMessage(LogLevel logLevel, string message, bool writeToPlatform);

        void PopMessage(LogLevel logLevel, string message, string note, bool writeToPlatform);

        void PushMessage(LogLevel logLevel, string message, bool writeToPlatform);

        void PushMessage(LogLevel logLevel, string message, string note, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);
    }
}