using RabbitMQ.Client;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using System;
using System.Threading.Tasks;

namespace SimpleLoggingClient.Dao
{
    public class RabbitMQ : IQueueMessenger
    {
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
            RoutingKey = initializationInformation.RabbitMq.RoutingKey;
            Queue = initializationInformation.RabbitMq.QueueName;
        }

        /// <summary>
        /// Send message to RabbitMQ queue
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async void SendMessage(byte[] message)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Logging failed: " + ex.Message);
            }
        }
    }
}