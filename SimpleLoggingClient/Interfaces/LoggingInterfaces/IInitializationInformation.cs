using SimpleLoggingClient.Entities.LoggingInformation;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Interfaces.LoggingInterfaces
{
    public interface IInitializationInformation
    {
        GcpMq GcpMq { get; set; }
        RabbitMq RabbitMq { get; set; }
        MessageQueueType MessageQueueType { get; set; }
        string ApplicationName { get; set; }
        LogLevel PublishLoggingLevel { get; set; }
        bool EncryptMessages { get; set; }
    }
}