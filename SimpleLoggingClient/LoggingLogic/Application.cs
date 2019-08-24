﻿using SimpleLoggingClient.Helper;
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
        private LogicHelper _logicHelper;
        private IQueueMessenger _queueMessenger;
        private readonly string _applicationName;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public Application(string applicationName)
        {
            _applicationName = applicationName;
            _logicHelper = new LogicHelper();
            _queueMessenger = new QueueMessenger();
        }

        public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IApplicationEntity application = new ApplicationEntity();
                application.Error = exception;
                application.OnlyInnerException = innerExceptionOnly;
                application.WrittenToPlatform = writeToPlatform;
                application.LogLevel = logLevel;
                application.Application = _applicationName;
                application.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(application);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Error(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IApplicationEntity application = new ApplicationEntity();
                application.Error = exception;
                application.OnlyInnerException = innerExceptionOnly;
                application.WrittenToPlatform = writeToPlatform;
                application.LogLevel = logLevel;
                application.Application = _applicationName;
                application.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(application);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Message(LogLevel logLevel, string message, string currentMethod, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IApplicationEntity application = new ApplicationEntity();
                application.ApplicationMessage = message;
                application.WrittenToPlatform = writeToPlatform;
                application.LogLevel = logLevel;
                application.CurrentMethod = currentMethod;
                application.Application = _applicationName;
                application.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(application);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Message(LogLevel logLevel, string message, string note, string currentMethod, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, message, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IApplicationEntity application = new ApplicationEntity();
                application.ApplicationMessage = message;
                application.WrittenToPlatform = writeToPlatform;
                application.LogLevel = logLevel;
                application.CurrentMethod = currentMethod;
                application.Application = _applicationName;
                application.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(application);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
    }
}