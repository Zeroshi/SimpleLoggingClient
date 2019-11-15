using Newtonsoft.Json;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingInterfaces.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Helper
{
    public class LogicHelper : ILogicHelper
    {
        private LogLevel _publishLoggingLevel;
        private bool _isEncrypted;

        public LogicHelper(IInitializationInformation initializationInformation)
        {
            _publishLoggingLevel = initializationInformation.PublishLoggingLevel;
            _isEncrypted = initializationInformation.EncryptMessages;
        }

        /// <summary>
        /// Send message to console (cloud logging)
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        /// <param name="note"></param>
        /// <param name="logToPlatform"></param>
        public void LogToPlatform(string messageType, string message, string note, bool logToPlatform)
        {
            if (logToPlatform)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Message: " + message);

                if (!string.IsNullOrEmpty(note))
                {
                    sb.Append(" Note: " + note);
                }

                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Send message to console (cloud logging)
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="note"></param>
        /// <param name="logToPlatform"></param>

        public void LogToPlatform(string messageType, string request, string response, string note, bool logToPlatform)
        {
            if (logToPlatform)
            {
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(request))
                {
                    sb.Append("Request: " + request);
                }

                if (!string.IsNullOrEmpty(response))
                {
                    sb.Append(" Response: " + response);
                }

                if (!string.IsNullOrEmpty(note))
                {
                    sb.Append(" Note: " + note);
                }

                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Send message to console (cloud logging)
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="exception"></param>
        /// <param name="innerExceptionOnly"></param>
        /// <param name="note"></param>
        /// <param name="logToPlatform"></param>
        public void LogToPlatform(string messageType, Exception exception, bool innerExceptionOnly, string note, bool logToPlatform)
        {
            if (logToPlatform)
            {
                StringBuilder sb = new StringBuilder();

                if (innerExceptionOnly)
                {
                    sb.Append("Exception: " + exception.InnerException);
                }
                else
                {
                    sb.Append("Exception: " + exception.Message);
                }

                if (!string.IsNullOrEmpty(note))
                {
                    sb.Append(" Note: " + note);
                }

                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Convert application object to json
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<byte[]> MessageConversion(IApplicationEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        /// <summary>
        /// Convert message queue object to json
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<byte[]> MessageConversion(IMessageQueueEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        /// <summary>
        /// Convert Internal/External transaction object to json
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<byte[]> MessageConversion(ITransactionEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        /// <summary>
        /// Convert relational database object to json
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<byte[]> MessageConversion(IRelationalDatabaseEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        /// <summary>
        /// Encode and optionally encrypt messages before sending to queue
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] MessageFormatting(string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);

            if (_isEncrypted)
            {
                return Encoding.UTF8.GetBytes(Convert.ToBase64String(encodedMessage));
            }

            return encodedMessage;
        }

        /// <summary>
        /// Determine if the
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool ShouldSendToQueue(LogLevel logLevel)
        {
            if ((int)_publishLoggingLevel > -1 || (int)_publishLoggingLevel != Convert.ToInt32(LogLevel.Off))
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (_publishLoggingLevel == LogLevel.Debug ||
                            _publishLoggingLevel == LogLevel.Info ||
                            _publishLoggingLevel == LogLevel.Error)
                        { return true; }
                        break;

                    case LogLevel.Info:
                        if (_publishLoggingLevel == LogLevel.Info ||
                            _publishLoggingLevel == LogLevel.Error)
                        { return true; }
                        break;

                    case LogLevel.Error:
                        if (_publishLoggingLevel == LogLevel.Error)
                        { return true; }
                        break;

                    default:
                        return false;
                }
            }

            return false;
        }
    }
}