﻿using Newtonsoft.Json;
using SimpleLoggingInterfaces.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Helper
{
    public class LogicHelper
    {
        private readonly int _environmentLoggingLevel;
        private readonly bool _isEncrypted;

        public LogicHelper()
        {
            _environmentLoggingLevel = Convert.ToInt32(Environment.GetEnvironmentVariable("Logging_Level"));
            _isEncrypted = Convert.ToBoolean(Environment.GetEnvironmentVariable("Is_Encrypted"));
        }

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

        public async Task<byte[]> MessageConversion(IApplicationEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        public async Task<byte[]> MessageConversion(IMessageQueueEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        public async Task<byte[]> MessageConversion(ITransactions entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        public async Task<byte[]> MessageConversion(IRelationalDatabaseEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return MessageFormatting(json);
            });

            return new byte[0];
        }

        private byte[] MessageFormatting(string message)
        {
            var encodedMessage = Encoding.UTF8.GetBytes(message);

            if (_isEncrypted)
            {
                return Encoding.UTF8.GetBytes(Convert.ToBase64String(encodedMessage));
            }

            return encodedMessage;
        }

        public bool ShouldSendToQueue(LogLevel logLevel)
        {
            if (_environmentLoggingLevel > -1 || _environmentLoggingLevel != Convert.ToInt32(LogLevel.Off))
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        if (_environmentLoggingLevel == Convert.ToInt32(LogLevel.Debug) ||
                            _environmentLoggingLevel == Convert.ToInt32(LogLevel.Info) ||
                            _environmentLoggingLevel == Convert.ToInt32(LogLevel.Error))
                        { return true; }
                        break;

                    case LogLevel.Info:
                        if (_environmentLoggingLevel == Convert.ToInt32(LogLevel.Info) ||
                            _environmentLoggingLevel == Convert.ToInt32(LogLevel.Error))
                        { return true; }
                        break;

                    case LogLevel.Error:
                        if (_environmentLoggingLevel == Convert.ToInt32(LogLevel.Error))
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