using SimpleLoggingClient.Helper;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingLogic
{
    public class Application : IApplication
    {
        private ILogicHelper _logicHelper;
        private IQueueMessenger _queueMessenger;
        private readonly string _applicationName;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public Application(string applicationName, Enums.Enums.MessageQueueType messageQueueType, ILogicHelper logHelper)
        {
            _applicationName = applicationName;
            _logicHelper = logHelper;
            _queueMessenger = new MessageRoutingType { }.MessageQueueSelection(messageQueueType);
        }

        public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(exception, innerExceptionOnly, writeToPlatform, logLevel, null);

                    var queueMessage = await _logicHelper.MessageConversion(application);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        public async void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(exception, innerExceptionOnly, writeToPlatform, logLevel, note);

                    var queueMessage = await _logicHelper.MessageConversion(application);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        public async void Message(LogLevel logLevel, string message, string currentMethod, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(message, writeToPlatform, logLevel, currentMethod, null);

                    var queueMessage = await _logicHelper.MessageConversion(application);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        public async void Message(LogLevel logLevel, string message, string note, string currentMethod, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, note, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(message, writeToPlatform, logLevel, currentMethod, note);

                    var queueMessage = await _logicHelper.MessageConversion(application);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        public IApplicationEntity PopulateApplicationEntity(string message, bool writeToPlatform, LogLevel logLevel, string currentMethod, string note)
        {
            IApplicationEntity application = new ApplicationEntity();
            application.ApplicationMessage = string.IsNullOrEmpty(message) ? string.Empty : message;
            application.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
            application.WrittenToPlatform = writeToPlatform;
            application.LogLevel = logLevel;
            application.CurrentMethod = string.IsNullOrEmpty(currentMethod) ? string.Empty : currentMethod;
            application.Application = _applicationName;
            application.DateTime = DateTime.UtcNow;

            return application;
        }

        public IApplicationEntity PopulateApplicationEntity(Exception exception, bool innerExceptionOnly, bool writeToPlatform, LogLevel logLevel, string note)
        {
            IApplicationEntity application = new ApplicationEntity();
            application.Error = exception;
            application.OnlyInnerException = innerExceptionOnly;
            application.WrittenToPlatform = writeToPlatform;
            application.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
            application.LogLevel = logLevel;
            application.Application = _applicationName;
            application.DateTime = DateTime.UtcNow;

            return application;
        }
    }
}