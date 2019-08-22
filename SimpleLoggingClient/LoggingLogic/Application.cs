using SimpleLoggingClient.Helper;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using System;
using static SimpleLoggingClient.LoggingInterfaces.Enums.Enums;

namespace SimpleLoggingClient.LoggingLogic
{
    public class Application : IApplication
    {
        private LogicHelper _logicHelper;
        private IQueueMessenger _queueMessenger;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public Application()
        {
            _logicHelper = new LogicHelper();
            _queueMessenger = new QueueMessenger();
        }

        public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            IApplicationEntity application = new ApplicationEntity();
            application.Error = exception;
            application.OnlyInnerException = innerExceptionOnly;
            application.WrittenToPlatform = writeToPlatform;
            application.LogLevel = logLevel;

            var queueMessage = await _logicHelper.MessageConversion(application);
            _queueMessenger.SendMessage(queueMessage);
        }

        public async void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            IApplicationEntity application = new ApplicationEntity();
            application.Error = exception;
            application.OnlyInnerException = innerExceptionOnly;
            application.WrittenToPlatform = writeToPlatform;
            application.LogLevel = logLevel;

            var queueMessage = await _logicHelper.MessageConversion(application);
            _queueMessenger.SendMessage(queueMessage);
        }

        public async void Message(LogLevel logLevel, string message, string currentMethod, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, null, writeToPlatform);

            IApplicationEntity application = new ApplicationEntity();
            application.ApplicationMessage = message;
            application.WrittenToPlatform = writeToPlatform;
            application.LogLevel = logLevel;
            application.CurrentMethod = currentMethod;

            var queueMessage = await _logicHelper.MessageConversion(application);
            _queueMessenger.SendMessage(queueMessage);
        }

        public async void Message(LogLevel logLevel, string message, string note, string currentMethod, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, note, writeToPlatform);

            IApplicationEntity application = new ApplicationEntity();
            application.ApplicationMessage = message;
            application.WrittenToPlatform = writeToPlatform;
            application.LogLevel = logLevel;
            application.CurrentMethod = currentMethod;

            var queueMessage = await _logicHelper.MessageConversion(application);
            _queueMessenger.SendMessage(queueMessage);
        }
    }
}