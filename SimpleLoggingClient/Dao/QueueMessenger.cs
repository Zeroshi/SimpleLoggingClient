using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace SimpleLoggingClient.LoggingInterfaces.Dao
{
    public class QueueMessenger : IQueueMessenger
    {
        private const string QUEUE_HOSTNAME = "Queue_Hostname";
        private const string QUEUE_USERNAME = "Queue_Username";
        private const string QUEUE_PASSWORD = "Queue_Password";
        private const string QUEUE_VIRTUALHOST = "Queue_Virtual_Host";
        private const string QUEUE_PORT = "Queue_Port";
        private const string QUEUE_EXCHANGENAME = "Queue_Exchange_Name";
        private const string QUEUE_ROUTINGKEY = "Queue_Routing_Key";
        private const string QUEUE_QUEUENAME = "Queue_Name";

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

            ExchangeName = Environment.GetEnvironmentVariable(QUEUE_EXCHANGENAME);
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