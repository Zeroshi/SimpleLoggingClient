using RabbitMQ.Client;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using System;
using System.Threading.Tasks;

namespace SimpleLoggingClient.Dao
{
    public class RabbitMq : IQueueMessenger
    {
        private string ExchangeName { get; set; }
        private string RoutingKey { get; set; }

        private ConnectionFactory _connectionfactory;

        public RabbitMq(IInitializationInformation initializationInformation)
        {
            _connectionfactory = new ConnectionFactory()
            {
                HostName = initializationInformation.RabbitMq.HostName,
                UserName = initializationInformation.RabbitMq.UserName,
                Password = initializationInformation.RabbitMq.Password,
                Port = initializationInformation.RabbitMq.PortNumber,
                DispatchConsumersAsync = true
            };

            ExchangeName = initializationInformation.RabbitMq.ExchangeName;
            RoutingKey = initializationInformation.RabbitMq.RoutingKey;

            RabbitMQSetup(initializationInformation);
        }

        private void RabbitMQSetup(IInitializationInformation initializationInformation)
        {
            var connection = _connectionfactory.CreateConnection();
            var model = connection.CreateModel();

            try
            {
                model.ExchangeDeclare(initializationInformation.RabbitMq.ExchangeName, ExchangeType.Direct);
                Console.WriteLine("Creating Exchange: " + initializationInformation.RabbitMq.ExchangeName);

                model.QueueDeclare(initializationInformation.RabbitMq.QueueName, true, false, false, null);
                Console.WriteLine("Creating Queue: " + initializationInformation.RabbitMq.QueueName);

                model.QueueBind(initializationInformation.RabbitMq.QueueName, initializationInformation.RabbitMq.ExchangeName,
                    initializationInformation.RabbitMq.RoutingKey);
                Console.WriteLine(string.Format("Creating Binding between Queue: {0} and Exchange: {1} using key: {2}",
                    initializationInformation.RabbitMq.QueueName, initializationInformation.RabbitMq.ExchangeName,
                    initializationInformation.RabbitMq.RoutingKey));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Send message to RabbitMQ queue
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendMessage(byte[] message)
        {
            return await Task.Run(() => SendLogic(message));
        }

        private bool SendLogic(byte[] message)
        {
            var connection = _connectionfactory.CreateConnection();
            var model = connection.CreateModel();

            try
            {
                model.BasicPublish(ExchangeName, RoutingKey, true, null, message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logging failed: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}