using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleLoggingClient.Entities.LoggingInformation;
using SimpleLoggingClient.Helper;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingEntities;
using SimpleLoggingClient.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using SimpleLoggingClient.LoggingInterfaces.Logic;
using SimpleLoggingClient.LoggingLogic;
using SimpleLoggingInterfaces.Interfaces;
using System;
using static SimpleLoggingInterfaces.Enums.EnumCollection;

namespace SimpleLoggingClient.Test
{
    [TestClass]
    public class SimpleLoggingTests
    {
        private const string APPLICATION_NAME = "Test";

        private ITransaction _internalTransaction;
        private ITransaction _externalTransaction;
        private IApplication _application;
        private IMessageQueue _messageQueue;
        private IRelationalDatabase _relationalDatabase;
        private IQueueMessenger _queueMessenger;
        private ILogicHelper _logicHelper;
        private IInitializationInformation _initializationInformation;

        public SimpleLoggingTests()
        {
            Mock<IQueueMessenger> queueMessenger = new Mock<IQueueMessenger>();
            queueMessenger.Setup(x => x.SendMessage(It.IsAny<byte[]>()));
            _queueMessenger = queueMessenger.Object;
            _initializationInformation = new InitialilizationInformation(MessageQueueType.RabbitMQ);
            _initializationInformation.RabbitMq.ExchangeName = "Testing";
            _initializationInformation.RabbitMq.HostName = "localHost";
            _initializationInformation.RabbitMq.Password = "guest";
            _initializationInformation.RabbitMq.UserName = "guest";
            _initializationInformation.RabbitMq.PortNumber = 5672;
            _initializationInformation.RabbitMq.QueueName = "SimpleLoggingClientTest";
            _initializationInformation.RabbitMq.RoutingKey = "key";
            _initializationInformation.ApplicationName = APPLICATION_NAME;
            _logicHelper = new LogicHelper(_initializationInformation);
            _internalTransaction = new InternalTransaction(_initializationInformation);
            _externalTransaction = new ExternalTransaction(_initializationInformation);
            _application = new Application(_initializationInformation);
            _messageQueue = new MessageQueue(_initializationInformation);
            _relationalDatabase = new RelationalDatabase(_initializationInformation);
        }

        [TestMethod]
        public void MessageQueueEntityPopPopulation()
        {
            var expected = new MessageQueueEntity();
            expected.Application = APPLICATION_NAME;
            expected.LogLevel = LogLevel.Debug;
            expected.Note = "note";
            expected.PopMessage = "popMessage";
            expected.PushMessage = string.Empty;
            expected.WrittenToPlatform = true;

            var result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, "popMessage", null, "note", true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.Error, result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);

            expected.Application = APPLICATION_NAME;
            expected.LogLevel = LogLevel.Debug;
            expected.Note = string.Empty;
            expected.PopMessage = "popMessage";
            expected.PushMessage = string.Empty;
            expected.WrittenToPlatform = true;

            result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, "popMessage", null, null, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.Error, result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
        }

        [TestMethod]
        public void MessageQueueEntityPushPopulation()
        {
            var expected = new MessageQueueEntity();
            expected.Application = APPLICATION_NAME;
            expected.LogLevel = LogLevel.Debug;
            expected.Note = "note";
            expected.PopMessage = string.Empty;
            expected.PushMessage = "pushMessage";
            expected.WrittenToPlatform = true;

            var result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, null, "pushMessage", "note", true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.Error, result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);

            expected.Application = APPLICATION_NAME;
            expected.LogLevel = LogLevel.Debug;
            expected.Note = string.Empty;
            expected.PopMessage = string.Empty;
            expected.PushMessage = "pushMessage";
            expected.WrittenToPlatform = true;

            result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, null, "pushMessage", null, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.Error, result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
        }

        [TestMethod]
        public void MessageQueueEntityErrorPopulation()
        {
            var expected = new MessageQueueEntity();
            expected.Application = APPLICATION_NAME;
            expected.LogLevel = LogLevel.Debug;
            expected.Note = "note";
            expected.PopMessage = string.Empty;
            expected.PushMessage = string.Empty;
            expected.WrittenToPlatform = true;
            expected.OnlyInnerException = true;
            expected.Error = new Exception();

            var result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, new Exception(), "note", true, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.PushMessage, result.PushMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);

            result = _messageQueue.PopulateMessageQueueEntity(LogLevel.Debug, new Exception(), null, true, true);

            expected.Note = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.PopMessage, result.PopMessage);
            Assert.AreEqual(expected.PushMessage, result.PushMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
        }

        [TestMethod]
        public void ApplicationPopulation()
        {
            IApplicationEntity expected = new ApplicationEntity();
            expected.Application = APPLICATION_NAME;
            expected.CurrentMethod = "Method()";
            expected.LogLevel = LogLevel.Debug;
            expected.Note = "note";
            expected.WrittenToPlatform = true;
            expected.ApplicationMessage = "message";

            var result = _application.PopulateApplicationEntity("message", true, LogLevel.Debug, "Method()", "note");

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.ApplicationMessage, result.ApplicationMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.CurrentMethod, result.CurrentMethod);

            expected.CurrentMethod = string.Empty;
            expected.Note = string.Empty;

            result = _application.PopulateApplicationEntity("message", true, LogLevel.Debug, null, null);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.ApplicationMessage, result.ApplicationMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.CurrentMethod, result.CurrentMethod);
        }

        [TestMethod]
        public void ApplicationErrorPopulation()
        {
            IApplicationEntity expected = new ApplicationEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;

            var result = _application.PopulateApplicationEntity(new Exception(), true, true, LogLevel.Debug, "note");

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.ApplicationMessage, result.ApplicationMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.CurrentMethod, result.CurrentMethod);

            result = _application.PopulateApplicationEntity(new Exception(), true, true, LogLevel.Debug, null);

            expected.Note = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.ApplicationMessage, result.ApplicationMessage);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.CurrentMethod, result.CurrentMethod);
        }

        [TestMethod]
        public void ExternalTransactionPopulation()
        {
            ITransactionEntity expected = new ExternalTransactionEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Response = "Response";
            expected.Request = "Request";
            expected.TrasactionType = TransactionType.External;
            expected.URI = "www.google.com";

            var result = _externalTransaction.PopulateTransactionEntity(LogLevel.Debug, "Request", "Response", "www.google.com", "note", true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _externalTransaction.PopulateTransactionEntity(LogLevel.Debug, null, null, null, null, true);

            expected.Note = string.Empty;
            expected.Response = string.Empty;
            expected.Request = string.Empty;
            expected.URI = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        [TestMethod]
        public void ExternalTransactionErrorPopulation()
        {
            ITransactionEntity expected = new ExternalTransactionEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Response = "Response";
            expected.Request = "Request";
            expected.TrasactionType = TransactionType.External;
            expected.URI = "www.google.com";

            var result = _externalTransaction.PopulateTransactionEntity(LogLevel.Debug, new Exception(), "Request", "Response", "www.google.com", "note", true, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _externalTransaction.PopulateTransactionEntity(LogLevel.Debug, new Exception(), null, null, null, null, true, true);

            expected.Note = string.Empty;
            expected.Response = string.Empty;
            expected.Request = string.Empty;
            expected.URI = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        [TestMethod]
        public void InternalTransactionPopulation()
        {
            ITransactionEntity expected = new ExternalTransactionEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Response = "Response";
            expected.Request = "Request";
            expected.TrasactionType = TransactionType.Internal;
            expected.URI = "www.google.com";

            var result = _internalTransaction.PopulateTransactionEntity(LogLevel.Debug, "Request", "Response", "www.google.com", "note", true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _internalTransaction.PopulateTransactionEntity(LogLevel.Debug, null, null, null, null, true);

            expected.Note = string.Empty;
            expected.Response = string.Empty;
            expected.Request = string.Empty;
            expected.URI = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        [TestMethod]
        public void InternalTransactionErrorPopulation()
        {
            ITransactionEntity expected = new ExternalTransactionEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Response = "Response";
            expected.Request = "Request";
            expected.TrasactionType = TransactionType.Internal;
            expected.URI = "www.google.com";

            var result = _internalTransaction.PopulateTransactionEntity(LogLevel.Debug, new Exception(), "Request", "Response", "www.google.com", "note", true, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _internalTransaction.PopulateTransactionEntity(LogLevel.Debug, new Exception(), null, null, null, null, true, true);

            expected.Note = string.Empty;
            expected.Response = string.Empty;
            expected.Request = string.Empty;
            expected.URI = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Response, result.Response);
            Assert.AreEqual(expected.Request, result.Request);
            Assert.AreEqual(expected.TrasactionType, result.TrasactionType);
            Assert.AreEqual(expected.URI, result.URI);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        [TestMethod]
        public void RelationalDatabasePopulation()
        {
            IRelationalDatabaseEntity expected = new RelationalDatabaseEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Query = "Query";
            expected.QueryResult = "Result";

            var result = _relationalDatabase.PopulateRelationalDatabaseEntity(LogLevel.Debug, "Query", "Result", "note", true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Query, result.Query);
            Assert.AreEqual(expected.QueryResult, result.QueryResult);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _relationalDatabase.PopulateRelationalDatabaseEntity(LogLevel.Debug, null, null, null, true);

            expected.Note = string.Empty;
            expected.Query = string.Empty;
            expected.QueryResult = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.AreEqual(expected.Query, result.Query);
            Assert.AreEqual(expected.QueryResult, result.QueryResult);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        [TestMethod]
        public void RelationalDatabaseErrorPopulation()
        {
            IRelationalDatabaseEntity expected = new RelationalDatabaseEntity();
            expected.OnlyInnerException = true;
            expected.WrittenToPlatform = true;
            expected.Note = "note";
            expected.LogLevel = LogLevel.Debug;
            expected.Application = APPLICATION_NAME;
            expected.Query = "Query";
            expected.QueryResult = "Result";

            var result = _relationalDatabase.PopulateRelationalDatabaseEntity(LogLevel.Debug, new Exception(), "note", true, true);

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);

            result = _relationalDatabase.PopulateRelationalDatabaseEntity(LogLevel.Debug, new Exception(), null, true, true);

            expected.Note = string.Empty;

            Assert.AreEqual(expected.Application, result.Application);
            Assert.AreEqual(expected.LogLevel, result.LogLevel);
            Assert.AreEqual(expected.Note, result.Note);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual(expected.OnlyInnerException, result.OnlyInnerException);
            Assert.AreEqual(expected.WrittenToPlatform, result.WrittenToPlatform);
        }

        //[TestMethod]
        //public void MessageRoutingTypeRabbitMQ()
        //{
        //    _initializationInformation.MessageQueueType = MessageQueueType.RabbitMQ;
        //    var messageType = new MessageRoutingType();
        //    var result = messageType.MessageQueueSelection(_initializationInformation);

        //    Assert.AreEqual(result.GetType(), new Dao.RabbitMq(_initializationInformation).GetType());
        //}

        //[TestMethod]
        //public void MessageRoutingTypeGcpMQ()
        //{
        //    _initializationInformation.MessageQueueType = MessageQueueType.GcpMQ;
        //    var messageType = new MessageRoutingType();
        //    var result = messageType.MessageQueueSelection(_initializationInformation);

        //    Assert.AreEqual(result.GetType(), new GcpMqEntity().GetType());
        //}

        //[TestMethod]
        //public void ApplicationTest()
        //{
        //    _initializationInformation.MessageQueueType = MessageQueueType.RabbitMQ;
        //    ILog logger = new SimpleLoggingClient.Log("LoggingTestAppConsole", "localHost", "guest", "guest", "LoggingTestAppConsole",
        //        5672, "ExhangeName", "Key", "QueueName", LogLevel.Debug, true);

        //    logger.Application.Message(LogLevel.Error, "This is a test", "Application Test", true);
        //}
    }
}