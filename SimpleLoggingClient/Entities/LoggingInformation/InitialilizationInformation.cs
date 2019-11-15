using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Entities.LoggingInformation
{
    public class InitialilizationInformation : IInitializationInformation
    {
        public GcpMq GcpMq { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public MessageQueueType MessageQueueType { get; set; }
        public string ApplicationName { get; set; }
        public LogLevel PublishLoggingLevel { get; set; }
        public bool EncryptMessages { get; set; }

        public InitialilizationInformation(MessageQueueType messageQueueType)
        {
            GcpMq = new GcpMq();
            RabbitMq = new RabbitMq();
        }
    }
}