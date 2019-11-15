using RabbitMQ.Client;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using System;
using System.Threading.Tasks;

namespace SimpleLoggingClient.Dao
{
    public class RabbitMQ : IQueueMessenger
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

        public RabbitMQ(IInitializationInformation initializationInformation)
        {
            _factory = new ConnectionFactory()
            {
                HostName = initializationInformation.RabbitMq.HostName,
                UserName = initializationInformation.RabbitMq.UserName,
                Password = initializationInformation.RabbitMq.Password,
                VirtualHost = initializationInformation.RabbitMq.VirtualHost,
                Port = initializationInformation.RabbitMq.PortNumber
            };

            ExchangeName = initializationInformation.RabbitMq.ExchangeName;
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