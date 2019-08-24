using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;
using System;

namespace SimpleLoggingClient
{
    public class Log : ILog
    {
        private string APPLICATION_NAME = "Application Name";

        public IMessageQueue MessageQueue { get; set; }
        public IApplication Application { get; set; }
        public ITransaction InternalTransaction { get; set; }
        public ITransaction ExternalTransaction { get; set; }

        public Log()
        {
            var applicationName = Environment.GetEnvironmentVariable(APPLICATION_NAME);
            MessageQueue = new MessageQueue(applicationName);
            Application = new Application(applicationName);
            InternalTransaction = new InternalTransaction(applicationName);
            ExternalTransaction = new ExternalTransaction(applicationName);
        }
    }
}