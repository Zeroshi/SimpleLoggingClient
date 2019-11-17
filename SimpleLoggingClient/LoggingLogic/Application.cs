using SimpleLoggingClient.Helper;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
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
        private IQueueMessenger _queueMessenger;
        private readonly string _applicationName;
        private ILogicHelper _logicHelper;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public Application(IInitializationInformation initializationInformation)
        {
            _logicHelper = new LogicHelper(initializationInformation);
            _applicationName = initializationInformation.ApplicationName;
            _queueMessenger = new MessageRoutingType { }.MessageQueueSelection(initializationInformation);
        }

        /// <summary>
        /// Application Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(exception, innerExceptionOnly, writeToPlatform, logLevel, null);

                    var queueMessage = _logicHelper.MessageConversion(application);
                    await _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Application Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="note"></param>
        /// <remarks>Specific note from services</remarks>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(exception, innerExceptionOnly, writeToPlatform, logLevel, note);

                    var queueMessage = _logicHelper.MessageConversion(application);
                    await _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Application message logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="currentMethod"></param>
        /// <param name="writeToPlatform"></param>
        public async void Message(LogLevel logLevel, string message, string currentMethod, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(message, writeToPlatform, logLevel, currentMethod, null);

                    var queueMessage = _logicHelper.MessageConversion(application);
                    await _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Application message logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="note"></param>
        /// <param name="currentMethod"></param>
        /// <param name="writeToPlatform"></param>
        public async void Message(LogLevel logLevel, string message, string note, string currentMethod, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, note, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var application = PopulateApplicationEntity(message, writeToPlatform, logLevel, currentMethod, note);

                    var queueMessage = _logicHelper.MessageConversion(application);
                    await _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Populate application object
        /// </summary>
        /// <param name="message"></param>
        /// <param name="writeToPlatform"></param>
        /// <param name="logLevel"></param>
        /// <param name="currentMethod"></param>
        /// <param name="note"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Populate application object
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        /// <param name="logLevel"></param>
        /// <param name="note"></param>
        /// <returns></returns>
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