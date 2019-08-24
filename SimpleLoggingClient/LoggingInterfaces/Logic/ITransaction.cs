using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingInterfaces.Logic
{
    public interface ITransaction
    {
        void Message(LogLevel logLevel, string request, string response, bool writeToPlatform);

        void Message(LogLevel logLevel, string request, string response, string uri, bool writeToPlatform);

        void Message(LogLevel logLevel, string request, string response, string uri, string note, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string request, string response, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, string note, bool innerExceptionOnly, bool writeToPlatform);
    }
}