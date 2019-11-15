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
    public class ExternalTransaction : ITransaction
    {
        private IQueueMessenger _queueMessenger;
        private readonly string _applicationName;
        private ILogicHelper _logicHelper;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public ExternalTransaction(IInitializationInformation initializationInformation)
        {
            _logicHelper = new LogicHelper(initializationInformation);
            _applicationName = initializationInformation.ApplicationName;
            _queueMessenger = new MessageRoutingType { }.MessageQueueSelection(initializationInformation);
        }

        /// <summary>
        /// External Transaction Error logging
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
                    var transaction = PopulateTransactionEntity(logLevel, exception, null, null, null, null, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// External Transaction Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="note"></param>
        /// <remarks>Specific note from services</remarks>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(LogLevel logLevel, Exception exception, string request, string response, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, exception, request, response, null, null, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// External Transaction Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, exception, request, response, uri, null, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Internal Transaction Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="note"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, note, writeToPlatform);
                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, exception, request, response, uri, note, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Internal Transaction message logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="writeToPlatform"></param>
        public async void Message(LogLevel logLevel, string request, string response, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, null, writeToPlatform);

            try
            {
                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, request, response, null, null, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Internal Transaction message logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="writeToPlatform"></param>
        public async void Message(LogLevel logLevel, string request, string response, string uri, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, null, writeToPlatform);

            try
            {
                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, request, response, uri, null, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Internal Transaction message logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="note"></param>
        /// <param name="writeToPlatform"></param>
        public async void Message(LogLevel logLevel, string request, string response, string uri, string note, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, note, writeToPlatform);

            try
            {
                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var transaction = PopulateTransactionEntity(logLevel, request, response, uri, note, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(transaction);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Populate Internal Transaction object
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="note"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        /// <returns></returns>
        public ITransactionEntity PopulateTransactionEntity(LogLevel logLevel, Exception exception, string request, string response, string uri, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            ITransactionEntity transaction = new ExternalTransactionEntity();
            transaction.Error = exception;
            transaction.LogLevel = logLevel;
            transaction.Request = string.IsNullOrWhiteSpace(request) ? string.Empty : request;
            transaction.Response = string.IsNullOrWhiteSpace(response) ? string.Empty : response;
            transaction.URI = string.IsNullOrWhiteSpace(uri) ? string.Empty : uri;
            transaction.Note = string.IsNullOrWhiteSpace(note) ? string.Empty : note;
            transaction.WrittenToPlatform = writeToPlatform;
            transaction.OnlyInnerException = innerExceptionOnly;
            transaction.TrasactionType = TransactionType.External;
            transaction.Application = _applicationName;
            transaction.DateTime = DateTime.UtcNow;

            return transaction;
        }

        /// <summary>
        /// Populate Internal Transaction object
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="uri"></param>
        /// <param name="note"></param>
        /// <param name="writeToPlatform"></param>
        /// <returns></returns>
        public ITransactionEntity PopulateTransactionEntity(LogLevel logLevel, string request, string response, string uri, string note, bool writeToPlatform)
        {
            ITransactionEntity transaction = new ExternalTransactionEntity();
            transaction.TrasactionType = TransactionType.External;
            transaction.Request = string.IsNullOrWhiteSpace(request) ? string.Empty : request;
            transaction.Response = string.IsNullOrWhiteSpace(response) ? string.Empty : response;
            transaction.URI = string.IsNullOrWhiteSpace(uri) ? string.Empty : uri;
            transaction.Note = string.IsNullOrWhiteSpace(note) ? string.Empty : note;
            transaction.WrittenToPlatform = writeToPlatform;
            transaction.LogLevel = logLevel;
            transaction.Application = _applicationName;
            transaction.DateTime = DateTime.UtcNow;

            return transaction;
        }
    }
}