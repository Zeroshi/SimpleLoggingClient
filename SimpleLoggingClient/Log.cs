using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;

namespace SimpleLoggingClient
{
    public class Log
    {
        public IMessageQueue MessageQueue;
        public IApplication Application;

        public Log()
        {
            MessageQueue = new MessageQueue();
            Application = new Application();
        }
    }
}