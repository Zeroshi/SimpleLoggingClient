using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleLoggingClient.LoggingInterfaces
{
    public interface IApplicationEntity : IEntityBase
    {
        string ApplicationMessage { get; set; }
        string CurrentMethod { get; set; }
    }
}