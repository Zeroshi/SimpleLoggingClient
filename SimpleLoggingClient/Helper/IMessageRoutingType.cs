using SimpleLoggingClient.LoggingInterfaces.Dao;

namespace SimpleLoggingClient.Helper
{
    public interface IMessageRoutingType
    {
        IQueueMessenger MessageQueueSelection(Enums.Enums.MessageQueueType messageQueueType);
    }
}