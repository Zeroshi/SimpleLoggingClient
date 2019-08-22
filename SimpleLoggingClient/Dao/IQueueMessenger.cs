using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLoggingClient.LoggingInterfaces.Dao
{
    public interface IQueueMessenger
    {
        void SendMessage(byte[] message);
    }
}