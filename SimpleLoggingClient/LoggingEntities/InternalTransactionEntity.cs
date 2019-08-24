﻿using SimpleLoggingInterfaces.Enums;
using SimpleLoggingInterfaces.Interfaces;
using System;

namespace SimpleLoggingClient.LoggingEntities
{
    internal class InternalTransactionEntity : ITransactions
    {
        public EnumCollection.TransactionType TrasactionType { get; set; }
        public string Request { get; set; }
        public string Reponse { get; set; }
        public string URI { get; set; }
        public bool WrittenToPlatform { get; set; }
        public bool OnlyInnerException { get; set; }
        public string Note { get; set; }
        public EnumCollection.LogLevel LogLevel { get; set; }
        public Exception Error { get; set; }
        public string Application { get; set; }
        public DateTime DateTime { get; set; }
    }
}