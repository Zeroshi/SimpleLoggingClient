using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;

namespace SimpleLoggingClient.Helper
{
    public interface IMessageRoutingType
    {
        IQueueMessenger MessageQueueSelection(IInitializationInformation initializationInformation);
    }
}