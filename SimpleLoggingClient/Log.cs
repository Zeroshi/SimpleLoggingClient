using SimpleLoggingClient.Helper;
using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;

namespace SimpleLoggingClient
{
    public class Log : ILog
    {
        public IMessageQueue MessageQueue { get; set; }
        public IApplication Application { get; set; }
        public ITransaction InternalTransaction { get; set; }
        public ITransaction ExternalTransaction { get; set; }
        public IRelationalDatabase RelationalDatabase { get; set; }

        public Log(string applicationName, Enums.Enums.MessageQueueType messageQueueType)
        {
            IMessageRoutingType messageRoutingType = new MessageRoutingType();
            ILogicHelper logicHelper = new LogicHelper();
            Application = new Application(applicationName, messageQueueType, logicHelper);
            InternalTransaction = new InternalTransaction(applicationName, messageQueueType, logicHelper);
            ExternalTransaction = new ExternalTransaction(applicationName, messageQueueType, logicHelper);
            RelationalDatabase = new RelationalDatabase(applicationName, messageQueueType, logicHelper);
        }
    }
}