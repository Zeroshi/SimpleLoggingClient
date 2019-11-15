using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.LoggingEntities
{
    public class RelationalDatabaseEntity : IRelationalDatabaseEntity
    {
        public string Query { get; set; }
        public string Database { get; set; }
        public string QueryResult { get; set; }
        public bool WrittenToPlatform { get; set; }
        public bool OnlyInnerException { get; set; }
        public string Note { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Error { get; set; }
        public string Application { get; set; }
        public DateTime DateTime { get; set; }
    }
}