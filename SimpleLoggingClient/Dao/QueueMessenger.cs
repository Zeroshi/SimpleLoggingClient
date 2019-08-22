using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace SimpleLoggingClient.LoggingInterfaces.Dao
{
    public class QueueMessenger : IQueueMessenger
    {
        private const string QUEUE_HOSTNAME = "Queue Hostname";
        private const string QUEUE_USERNAME = "Queue Username";
        private const string QUEUE_PASSWORD = "Queue Password";
        private const string QUEUE_VIRTUALHOST = "Queue Virtual Host";
        private const string QUEUE_PORT = "Queue Port";
        private const string QUEUE_ECHANGENAME = "Queue Echange Name";
        private const string QUEUE_ROUTINGKEY = "Queue Routing Key";
        private const string QUEUE_QUEUENAME = "Queue Name";

        private string ExchangeName { get; set; }
        private string RoutingKey { get; set; }
        private string Queue { get; set; }

        private ConnectionFactory _factory;

        public QueueMessenger()
        {
            _factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable(QUEUE_HOSTNAME),
                UserName = Environment.GetEnvironmentVariable(QUEUE_USERNAME),
                Password = Environment.GetEnvironmentVariable(QUEUE_PASSWORD),
                VirtualHost = Environment.GetEnvironmentVariable(QUEUE_VIRTUALHOST),
                Port = Convert.ToInt32(Environment.GetEnvironmentVariable(QUEUE_PORT))
            };

            ExchangeName = Environment.GetEnvironmentVariable(QUEUE_ECHANGENAME);
            RoutingKey = Environment.GetEnvironmentVariable(QUEUE_ROUTINGKEY);
            Queue = Environment.GetEnvironmentVariable(QUEUE_QUEUENAME);
        }

        public async void SendMessage(byte[] message)
        {
            await Task.Run(() =>
             {
                 using (var connection = _factory.CreateConnection())
                 {
                     using (var channel = connection.CreateModel())
                     {
                         channel.BasicPublish(ExchangeName, RoutingKey, true, null, message);
                     }
                 }
             });
        }
    }
}