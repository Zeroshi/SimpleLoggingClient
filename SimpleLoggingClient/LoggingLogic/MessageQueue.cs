using SimpleLoggingClient.Helper;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

public class MessageQueue : IMessageQueue
{
    private IQueueMessenger _queueMessenger;
    private readonly string _applicationName;

    private string PUSH_MESSAGE_TYPE = "Push Message";
    private string POP_MESSAGE_TYPE = "Pop Message";
    private string MQ_ERROR = "Message Queue Error";
    private ILogicHelper _logicHelper;

    public MessageQueue(IInitializationInformation initializationInformation)
    {
        _logicHelper = new LogicHelper(initializationInformation);
        _applicationName = initializationInformation.ApplicationName;
        _queueMessenger = new MessageRoutingType { }.MessageQueueSelection(initializationInformation);
    }

    /// <summary>
    /// Log Message popped from message queue
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    /// <param name="writeToPlatform"></param>
    public async void PopMessage(LogLevel logLevel, string message, bool writeToPlatform)
    {
        _logicHelper.LogToPlatform(POP_MESSAGE_TYPE, message, null, writeToPlatform);

        try
        {
            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, message, null, null, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Log Message popped from message queue
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    /// <param name="note"></param>
    /// <param name="writeToPlatform"></param>
    public async void PopMessage(LogLevel logLevel, string message, string note, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(POP_MESSAGE_TYPE, message, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, message, null, note, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Log Message being sent to message queue
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    /// <param name="writeToPlatform"></param>
    public async void PushMessage(LogLevel logLevel, string message, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(PUSH_MESSAGE_TYPE, message, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, null, message, null, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Log Message being sent to message queue
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="message"></param>
    /// <param name="note"></param>
    /// <param name="writeToPlatform"></param>
    public async void PushMessage(LogLevel logLevel, string message, string note, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(PUSH_MESSAGE_TYPE, message, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, null, message, note, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Message Queue Error logging
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="exception"></param>
    /// <param name="innerExceptionOnly"></param>
    /// <param name="writeToPlatform"></param>
    public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(MQ_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, exception, null, innerExceptionOnly, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Message Queue Error logging
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
            _logicHelper.LogToPlatform(MQ_ERROR, exception, innerExceptionOnly, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                var messageQueue = PopulateMessageQueueEntity(logLevel, exception, note, innerExceptionOnly, writeToPlatform);

                var queueMessage = _logicHelper.MessageConversion(messageQueue);
                await _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    /// <summary>
    /// Populate message queue object
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="popMessage"></param>
    /// <param name="pushMessage"></param>
    /// <param name="note"></param>
    /// <param name="writeToPlatform"></param>
    /// <returns></returns>
    public IMessageQueueEntity PopulateMessageQueueEntity(LogLevel logLevel, string popMessage, string pushMessage, string note, bool writeToPlatform)
    {
        IMessageQueueEntity messageQueue = new MessageQueueEntity();
        messageQueue.PopMessage = string.IsNullOrEmpty(popMessage) ? string.Empty : popMessage;
        messageQueue.PushMessage = string.IsNullOrEmpty(pushMessage) ? string.Empty : pushMessage;
        messageQueue.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
        messageQueue.WrittenToPlatform = writeToPlatform;
        messageQueue.LogLevel = logLevel;
        messageQueue.Application = _applicationName;
        messageQueue.DateTime = DateTime.UtcNow;

        return messageQueue;
    }

    /// <summary>
    /// Populate message queue object
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="exception"></param>
    /// <param name="note"></param>
    /// <param name="innerExceptionOnly"></param>
    /// <param name="writeToPlatform"></param>
    /// <returns></returns>
    public IMessageQueueEntity PopulateMessageQueueEntity(LogLevel logLevel, Exception exception, string note, bool innerExceptionOnly, bool writeToPlatform)
    {
        IMessageQueueEntity messageQueue = new MessageQueueEntity();
        messageQueue.Error = exception;
        messageQueue.LogLevel = logLevel;
        messageQueue.PopMessage = string.Empty;
        messageQueue.PushMessage = string.Empty;
        messageQueue.Note = string.IsNullOrEmpty(note) ? string.Empty : note;
        messageQueue.WrittenToPlatform = writeToPlatform;
        messageQueue.OnlyInnerException = innerExceptionOnly;
        messageQueue.Application = _applicationName;
        messageQueue.DateTime = DateTime.UtcNow;

        return messageQueue;
    }
}