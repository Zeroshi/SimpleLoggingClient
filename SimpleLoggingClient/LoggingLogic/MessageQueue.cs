using SimpleLoggingClient.Helper;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

public class MessageQueue : IMessageQueue
{
    private LogicHelper _logicHelper;
    private IQueueMessenger _queueMessenger;
    private readonly string _applicationName;

    private string PUSH_MESSAGE_TYPE = "Push Message";
    private string POP_MESSAGE_TYPE = "Pop Message";
    private string MQ_ERROR = "Message Queue Error";

    public MessageQueue(string applicationName)
    {
        _applicationName = applicationName;
        _logicHelper = new LogicHelper();
        _queueMessenger = new QueueMessenger();
    }

    public async void PopMessage(LogLevel logLevel, string message, bool writeToPlatform)
    {
        _logicHelper.LogToPlatform(POP_MESSAGE_TYPE, message, null, writeToPlatform);

        try
        {
            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.PopMessage = message;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    public async void PopMessage(LogLevel logLevel, string message, string note, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(POP_MESSAGE_TYPE, message, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.PopMessage = message;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.Note = note;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    public async void PushMessage(LogLevel logLevel, string message, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(PUSH_MESSAGE_TYPE, message, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.PushMessage = message;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    public async void PushMessage(LogLevel logLevel, string message, string note, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(PUSH_MESSAGE_TYPE, message, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.PushMessage = message;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.Note = note;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }

    public async void Error(LogLevel logLevel, Exception exception, bool innerExceptionOnly, bool writeToPlatform)
    {
        try
        {
            _logicHelper.LogToPlatform(MQ_ERROR, exception, innerExceptionOnly, null, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.Error = exception;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.OnlyInnerException = innerExceptionOnly;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
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
            _logicHelper.LogToPlatform(MQ_ERROR, exception, innerExceptionOnly, note, writeToPlatform);

            if (_logicHelper.ShouldSendToQueue(logLevel))
            {
                IMessageQueueEntity messageQueue = new MessageQueueEntity();
                messageQueue.Error = exception;
                messageQueue.Note = note;
                messageQueue.WrittenToPlatform = writeToPlatform;
                messageQueue.OnlyInnerException = innerExceptionOnly;
                messageQueue.LogLevel = logLevel;
                messageQueue.Application = _applicationName;
                messageQueue.DateTime = DateTime.UtcNow;

                var queueMessage = await _logicHelper.MessageConversion(messageQueue);
                _queueMessenger.SendMessage(queueMessage);
            }
        }
        catch (Exception ex)
        {
            _logicHelper.LogToPlatform(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, ex, false, null, true);
        }
    }
}