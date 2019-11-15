namespace SimpleLoggingClient.Entities.LoggingInformation
{
    public class RabbitMq
    {
        internal string HostName { get; set; }
        internal string UserName { get; set; }
        internal string Password { get; set; }
        internal string VirtualHost { get; set; }
        internal int PortNumber { get; set; }
        internal string ExchangeName { get; set; }
        internal string RoutingKey { get; set; }
        internal string QueueName { get; set; }
    }
}