﻿using SimpleLoggingClient.Entities.LoggingInformation;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient
{
    public class Log : ILog
    {
        public IMessageQueue MessageQueue { get; set; }
        public IApplication Application { get; set; }
        public ITransaction InternalTransaction { get; set; }
        public ITransaction ExternalTransaction { get; set; }
        public IRelationalDatabase RelationalDatabase { get; set; }

        /// <summary>
        /// GCP Pub/Sub Topic setup
        /// </summary>
        /// <remarks>This is used for GCP Pub/Sub</remarks>
        /// <param name="applicationName">Adds application name to all of the messages</param>
        /// <param name="projectId">GCP Project Id</param>
        /// <param name="topicId">GCP Topic Id</param>
        /// <param name="publishLogLevel">Level of logging that will be delivered (debug, info, error, off (no messages))</param>
        /// <param name="encryptMessages">encrypt messages prior to passing to queue</param>
        public Log(string applicationName, string projectId, string topicId, LogLevel publishLogLevel, bool encryptMessages)
        {
            IInitializationInformation initilizationInformation = new InitialilizationInformation(MessageQueueType.GcpMQ);
            initilizationInformation.GcpMq.ProjectId = projectId;
            initilizationInformation.GcpMq.TopicId = topicId;
            initilizationInformation.ApplicationName = applicationName;
            initilizationInformation.PublishLoggingLevel = publishLogLevel;
            initilizationInformation.EncryptMessages = encryptMessages;

            Initialize(initilizationInformation);
        }

        /// <summary>
        /// RabbitMQ setup
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="hostName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="virtualHost"></param>
        /// <param name="portNumber"></param>
        /// <param name="exchangeName"></param>
        /// <param name="routingKey"></param>
        /// <param name="queueName"></param>
        /// <param name="publishLogLevel"></param>
        /// <param name="encryptMessages"></param>
        public Log(string applicationName, string hostName, string userName, string password,
            string virtualHost, int portNumber, string exchangeName, string routingKey, string queueName,
            LogLevel publishLogLevel, bool encryptMessages)
        {
            IInitializationInformation initilizationInformation = new InitialilizationInformation(MessageQueueType.RabbitMQ);
            initilizationInformation.RabbitMq.HostName = hostName;
            initilizationInformation.RabbitMq.UserName = userName;
            initilizationInformation.RabbitMq.Password = password;
            initilizationInformation.RabbitMq.VirtualHost = virtualHost;
            initilizationInformation.RabbitMq.PortNumber = portNumber;
            initilizationInformation.RabbitMq.ExchangeName = exchangeName;
            initilizationInformation.RabbitMq.RoutingKey = routingKey;
            initilizationInformation.RabbitMq.QueueName = queueName;
            initilizationInformation.ApplicationName = applicationName;
            initilizationInformation.PublishLoggingLevel = publishLogLevel;
            initilizationInformation.EncryptMessages = encryptMessages;

            Initialize(initilizationInformation);
        }

        /// <summary>
        /// Initialize the application
        /// </summary>
        /// <param name="initializationInformation"></param>
        private void Initialize(IInitializationInformation initializationInformation)
        {
            Application = new Application(initializationInformation);
            InternalTransaction = new InternalTransaction(initializationInformation);
            ExternalTransaction = new ExternalTransaction(initializationInformation);
            RelationalDatabase = new RelationalDatabase(initializationInformation);
        }
    }
}