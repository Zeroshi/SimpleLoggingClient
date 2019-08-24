using SimpleLoggingClient.LoggingInterfaces.Logic;

namespace SimpleLoggingClient.LoggingInterfaces
{
    public interface ILog
    {
        IMessageQueue MessageQueue { get; set; }
        IApplication Application { get; set; }
    }
}