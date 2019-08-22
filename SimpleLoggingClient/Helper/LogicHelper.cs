using Newtonsoft.Json;
using SimpleLoggingInterfaces.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLoggingClient.Helper
{
    public class LogicHelper
    {
        public void LogToPlatform(string messageType, string message, string note, bool logToPlatform)
        {
            if (logToPlatform)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Message: " + message);

                if (!string.IsNullOrEmpty(note))
                {
                    sb.Append(" Note : " + note);
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
                    sb.Append(" Note : " + note);
                }

                Console.WriteLine(sb.ToString());
            }
        }

        public async Task<byte[]> MessageConversion(IApplicationEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return Encoding.UTF8.GetBytes(json);
            });

            return new byte[0];
        }

        public async Task<byte[]> MessageConversion(IMessageQueueEntity entity)
        {
            await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(entity);
                return Encoding.UTF8.GetBytes(json);
            });

            return new byte[0];
        }
    }
}