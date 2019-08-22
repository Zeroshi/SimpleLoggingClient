using System;

namespace SimpleLoggingClient.LoggingInterfaces
{
    public interface IMessageQueueEntity : IEntityBase
    {
        string PopMessage { get; set; }
        string PushMessage { get; set; }
    }
}