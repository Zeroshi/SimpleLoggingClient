using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingInterfaces.Logic
{
    public interface IRelationalDatabase
    {
        void Query(LogLevel logLevel, string query, bool writeToPlatform);

        void Query(LogLevel logLevel, string query, string note, bool writeToPlatform);

        void Result(LogLevel logLevel, string result, bool writeToPlatform);

        void Result(LogLevel logLevel, string result, string note, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform);

        void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);

        IRelationalDatabaseEntity PopulateRelationalDatabaseEntity(LogLevel logLevel, string query, string result, string note, bool writeToPlatform);

        IRelationalDatabaseEntity PopulateRelationalDatabaseEntity(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);
    }
}