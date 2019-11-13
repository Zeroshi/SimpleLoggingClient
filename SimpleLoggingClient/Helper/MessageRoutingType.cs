using SimpleLoggingClient.LoggingInterfaces.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLoggingClient.Helper
{
    public class MessageRoutingType : IMessageRoutingType
    {
        public IQueueMessenger MessageQueueSelection(Enums.Enums.MessageQueueType messageQueueType)
        {
            if (messageQueueType == Enums.Enums.MessageQueueType.RabbitMQ)
            {
                return new SimpleLoggingClient.Dao.RabbitMQ();
            }
            if (messageQueueType == Enums.Enums.MessageQueueType.GcpMQ)
            {
                return new SimpleLoggingClient.Dao.GCPMQ();
            }

            return null;
        }
    }
}