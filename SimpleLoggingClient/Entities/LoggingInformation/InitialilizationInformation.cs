using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Entities.LoggingInformation
{
    public class InitialilizationInformation : IInitializationInformation
    {
        public GcpMqEntity GcpMq { get; set; }
        public RabbitMqEntity RabbitMq { get; set; }
        public MessageQueueType MessageQueueType { get; set; }
        public string ApplicationName { get; set; }
        public LogLevel PublishLoggingLevel { get; set; }
        public bool EncryptMessages { get; set; }

        public InitialilizationInformation(MessageQueueType messageQueueType)
        {
            GcpMq = new GcpMqEntity();
            RabbitMq = new RabbitMqEntity();
            MessageQueueType = messageQueueType;
        }
    }
}