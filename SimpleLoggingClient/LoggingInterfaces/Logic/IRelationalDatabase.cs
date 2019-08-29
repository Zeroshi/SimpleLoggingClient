using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingInterfaces.Logic
{
    public interface IRelationalDatabase
    {
        void Transaction(LogLevel logLevel, string query, string result, bool writeToPlatform);

        void Transaction(LogLevel logLevel, string query, string result, string note, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);

        IRelationalDatabase PopulateApplicationEntity(LogLevel logLevel, string query, string result, string note, bool writeToPlatform);

        IRelationalDatabase PopulateApplicationEntity(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);
    }
}