using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingEntities
{
    internal class MessageQueueEntity : IMessageQueueEntity
    {
        public string PopMessage { get; set; }
        public string PushMessage { get; set; }
        public Exception Error { get; set; }
        public bool WrittenToPlatform { get; set; }
        public bool OnlyInnerException { get; set; }
        public string Note { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Application { get; set; }
        public DateTime DateTime { get; set; }
    }
}