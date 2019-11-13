using Google.Cloud.PubSub.V1;
using SimpleLoggingClient.Helper;
using SimpleLoggingClient.LoggingInterfaces.Dao;
using System;

namespace SimpleLoggingClient.Dao
{
    public class GCPMQ : IQueueMessenger
    {
        private readonly string _projectId;
        private readonly string _topicId;
        private readonly LogicHelper _LogicHelper;

        private const string MESSAGE_PUBLISHED = "Message Published";

        public GCPMQ()
        {
            _projectId = Environment.GetEnvironmentVariable("Google_ProjectId");
            _topicId = Environment.GetEnvironmentVariable("Google_TopicId");
            _LogicHelper = new LogicHelper();
        }

        public async void SendMessage(byte[] message)
        {
            var publisher = await PublisherClient.CreateAsync(new TopicName(_projectId, _topicId));
            var confirmation = await publisher.PublishAsync(message);

            _LogicHelper.LogToPlatform(MESSAGE_PUBLISHED, confirmation, null, true);
        }
    }
}