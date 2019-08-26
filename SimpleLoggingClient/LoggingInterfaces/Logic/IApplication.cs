using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingInterfaces.Logic
{
    public interface IApplication
    {
        void Message(LogLevel logLevel, string message, string currentmethod, bool writeToPlatform);

        void Message(LogLevel logLevel, string message, string note, string currentmethod, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);

        IApplicationEntity PopulateApplicationEntity(string message, bool writeToPlatform, LogLevel logLevel, string currentMethod, string note);

        IApplicationEntity PopulateApplicationEntity(Exception exception, bool innerExceptionOnly, bool writeToPlatform, LogLevel logLevel, string note);
    }
}