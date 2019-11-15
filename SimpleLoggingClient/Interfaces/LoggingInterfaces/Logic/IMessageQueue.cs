using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

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

        IMessageQueueEntity PopulateMessageQueueEntity(LogLevel logLevel, string popMessage, string pushMessage, string note, bool writeToPlatform);

        IMessageQueueEntity PopulateMessageQueueEntity(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform);
    }
}