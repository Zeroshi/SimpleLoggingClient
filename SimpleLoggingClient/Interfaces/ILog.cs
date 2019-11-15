using SimpleLoggingClient.LoggingInterfaces.Logic;

namespace SimpleLoggingClient.LoggingInterfaces
{
    public interface ILog
    {
        IMessageQueue MessageQueue { get; set; }
        IApplication Application { get; set; }
        ITransaction InternalTransaction { get; set; }
        ITransaction ExternalTransaction { get; set; }
        IRelationalDatabase RelationalDatabase { get; set; }
    }
}