using SimpleLoggingClient.Helper;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingInterfaces.Enums;
using SimpleLoggingInterfaces.Interfaces;
using System;

namespace SimpleLoggingClient.LoggingLogic
{
    public class RelationalDatabase : IRelationalDatabase
    {
        private readonly IQueueMessenger _queueMessenger;
        private readonly string _applicationName;
        private ILogicHelper _logicHelper;

        private const string QUERY_MESSAGE = "Query Message";
        private const string RESULT_MESSAGE = "Result Message";
        private const string DATABASE_ERROR = "Database Error";

        public RelationalDatabase(IInitializationInformation initializationInformation)
        {
            _logicHelper = new LogicHelper(initializationInformation);
            _applicationName = initializationInformation.ApplicationName;
            _queueMessenger = new MessageRoutingType { }.MessageQueueSelection(initializationInformation);
        }

        /// <summary>
        /// Relation Database Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(EnumCollection.LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(DATABASE_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, exception, null, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Relation Database Error logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="note"></param>
        /// <remarks>Specific note from services</remarks>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        public async void Error(EnumCollection.LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(DATABASE_ERROR, exception, innerExceptionOnly, note, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, exception, note, innerExceptionOnly, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Log query
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="query"></param>
        /// <param name="writeToPlatform"></param>
        public async void Query(EnumCollection.LogLevel logLevel, string query, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(QUERY_MESSAGE, query, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, query, null, null, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Log query
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="query"></param>
        /// <param name="note"></param>
        /// <param name="writeToPlatform"></param>
        public async void Query(EnumCollection.LogLevel logLevel, string query, string note, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(QUERY_MESSAGE, query, note, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, query, null, null, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Log database query result
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="result"></param>
        /// <param name="writeToPlatform"></param>
        public async void Result(EnumCollection.LogLevel logLevel, string result, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(RESULT_MESSAGE, result, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, null, result, null, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Log database query result
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="result"></param>
        /// <param name="note"></param>
        /// <param name="writeToPlatform"></param>
        public async void Result(EnumCollection.LogLevel logLevel, string result, string note, bool writeToPlatform)
        {
            try
            {
                _logicHelper.LogToPlatform(RESULT_MESSAGE, result, null, writeToPlatform);

                if (_logicHelper.ShouldSendToQueue(logLevel))
                {
                    var relationalDatabase = PopulateRelationalDatabaseEntity(logLevel, null, result, note, writeToPlatform);

                    var queueMessage = await _logicHelper.MessageConversion(relationalDatabase);
                    _queueMessenger.SendMessage(queueMessage);
                }
            }
            catch (Exception ex)
            {
                _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
            }
        }

        /// <summary>
        /// Populate relational database object for logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="query"></param>
        /// <param name="result"></param>
        /// <param name="note"></param>
        /// <param name="writeToPlatform"></param>
        /// <returns></returns>
        public IRelationalDatabaseEntity PopulateRelationalDatabaseEntity(EnumCollection.LogLevel logLevel, string query, string result, string note, bool writeToPlatform)
        {
            IRelationalDatabaseEntity messageQueue = new RelationalDatabaseEntity();
            messageQueue.Query = string.IsNullOrEmpty(query) ? string.Empty : query;
            messageQueue.QueryResult = string.IsNullOrEmpty(result) ? string.Empty : result;
            messageQueue.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
            messageQueue.WrittenToPlatform = writeToPlatform;
            messageQueue.LogLevel = logLevel;
            messageQueue.Application = _applicationName;
            messageQueue.DateTime = DateTime.UtcNow;

            return messageQueue;
        }

        /// <summary>
        /// Populate relational database object for logging
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="note"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="writeToPlatform"></param>
        /// <returns></returns>
        public IRelationalDatabaseEntity PopulateRelationalDatabaseEntity(EnumCollection.LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
        {
            IRelationalDatabaseEntity messageQueue = new RelationalDatabaseEntity();
            messageQueue.Error = exception;
            messageQueue.LogLevel = logLevel;
            messageQueue.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
            messageQueue.WrittenToPlatform = writeToPlatform;
            messageQueue.OnlyInnerException = innerExceptionOnly;
            messageQueue.Application = _applicationName;
            messageQueue.DateTime = DateTime.UtcNow;

            return messageQueue;
        }
    }
}