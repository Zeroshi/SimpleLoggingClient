using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleLoggingClient.LoggingEntities;
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

        public SimpleLoggingTests()
        {
            _internalTransaction = new InternalTransaction(APPLICATION_NAME);
            _externalTransaction = new ExternalTransaction(APPLICATION_NAME);
            _application = new Application(APPLICATION_NAME);
            _messageQueue = new MessageQueue(APPLICATION_NAME);
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
            ITransactions expected = new ExternalTransactionEntity();
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
            ITransactions expected = new ExternalTransactionEntity();
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
            ITransactions expected = new ExternalTransactionEntity();
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
            ITransactions expected = new ExternalTransactionEntity();
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
    }
}