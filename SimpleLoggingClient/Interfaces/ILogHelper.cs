using SimpleLoggingInterfaces.Interfaces;
using System;
using System.Threading.Tasks;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Helper
{
    public interface ILogicHelper
    {
        void LogToPlatform(string messageType, string message, string note, bool logToPlatform);

        void LogToPlatform(string messageType, string request, string response, string note, bool logToPlatform);

        void LogToPlatform(string messageType, Exception exception, bool innerExceptionOnly, string note, bool logToPlatform);

        byte[] MessageConversion(IApplicationEntity entity);

        byte[] MessageConversion(IMessageQueueEntity entity);

        byte[] MessageConversion(ITransactionEntity entity);

        byte[] MessageConversion(IRelationalDatabaseEntity entity);

        bool ShouldSendToQueue(LogLevel logLevel);
    }
}