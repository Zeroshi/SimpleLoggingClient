using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Helper
{
    public class MessageRoutingType : IMessageRoutingType
    {
        /// <summary>
        /// Determines the message service being used. Such as GCP Pub/Sub, RabbitMQ
        /// </summary>
        /// <param name="initializationInformation"></param>
        /// <returns></returns>
        public IQueueMessenger MessageQueueSelection(IInitializationInformation initializationInformation)
        {
            if (initializationInformation.MessageQueueType == MessageQueueType.RabbitMQ)
            {
                return new Dao.RabbitMq(initializationInformation);
            }
            if (initializationInformation.MessageQueueType == MessageQueueType.GcpMQ)
            {
                return new Dao.GCPMQ(initializationInformation);
            }

            return null;
        }
    }
}