using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;

namespace SimpleLoggingClient
{
    public class Log : ILog
    {
        public IMessageQueue MessageQueue { get; set; }
        public IApplication Application { get; set; }

        public Log()
        {
            MessageQueue = new MessageQueue();
            Application = new Application();
        }
    }
}