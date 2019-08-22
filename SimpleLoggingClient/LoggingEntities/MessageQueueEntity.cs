using SimpleLoggingClient.LoggingInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static SimpleLoggingClient.LoggingInterfaces.Enums.Enums;

namespace SimpleLoggingClient.LoggingEntities
{
    public class MessageQueueEntity : IMessageQueueEntity, IEntityBase
    {
        public string PopMessage { get; set; }
        public string PushMessage { get; set; }
        public Exception Error { get; set; }
        public bool WrittenToPlatform { get; set; }
        public bool OnlyInnerException { get; set; }
        public string Note { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}