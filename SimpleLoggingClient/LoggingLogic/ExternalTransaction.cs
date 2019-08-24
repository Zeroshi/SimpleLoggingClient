using SimpleLoggingClient.Helper;
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
        private LogicHelper _logicHelper;
        private IQueueMessenger _queueMessenger;
        private readonly string _applicationName;

        private const string APPLICATION_MESSAGE = "Application Message";
        private const string APPLICATION_ERROR = "Application Error";

        public ExternalTransaction(string applicationName)
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
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.Error = exception;
                transaction.LogLevel = logLevel;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.OnlyInnerException = innerExceptionOnly;
                transaction.TrasactionType = TransactionType.External;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Error(LogLevel logLevel, Exception exception, string request, string response, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.Error = exception;
                transaction.LogLevel = logLevel;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.OnlyInnerException = innerExceptionOnly;
                transaction.TrasactionType = TransactionType.External;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.Error = exception;
                transaction.LogLevel = logLevel;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.URI = uri;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.OnlyInnerException = innerExceptionOnly;
                transaction.TrasactionType = TransactionType.External;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Error(LogLevel logLevel, Exception exception, string request, string response, string uri, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_ERROR, exception, innerExceptionOnly, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.Error = exception;
                transaction.LogLevel = logLevel;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.URI = uri;
                transaction.Note = note;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.OnlyInnerException = innerExceptionOnly;
                transaction.TrasactionType = TransactionType.External;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Message(LogLevel logLevel, string request, string response, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.TrasactionType = TransactionType.External;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.LogLevel = logLevel;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Message(LogLevel logLevel, string request, string response, string uri, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.TrasactionType = TransactionType.External;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.URI = uri;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.LogLevel = logLevel;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }

        public async void Message(LogLevel logLevel, string request, string response, string uri, string note, bool writeToPlatform)
        {
            _logicHelper.LogToPlatform(APPLICATION_MESSAGE, request, response, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                ITransactions transaction = new ExternalTransactionEntity();
                transaction.TrasactionType = TransactionType.External;
                transaction.Request = request;
                transaction.Reponse = response;
                transaction.URI = uri;
                transaction.Note = note;
                transaction.WrittenToPlatform = writeToPlatform;
                transaction.LogLevel = logLevel;
                transaction.Application = _applicationName;
                transaction.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(transaction);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
    }
}