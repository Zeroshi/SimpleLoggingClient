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

        Task<byte[]> MessageConversion(IApplicationEntity entity);

        Task<byte[]> MessageConversion(IMessageQueueEntity entity);

        Task<byte[]> MessageConversion(ITransactionEntity entity);

        Task<byte[]> MessageConversion(IRelationalDatabaseEntity entity);

        bool ShouldSendToQueue(LogLevel logLevel);
    }
}