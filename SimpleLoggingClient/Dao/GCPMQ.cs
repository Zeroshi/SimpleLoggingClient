﻿using Google.Cloud.PubSub.V1;
using SimpleLoggingClient.Helper;
using SimpleLoggingClient.Interfaces.LoggingInterfaces;
using SimpleLoggingClient.LoggingInterfaces.Dao;

namespace SimpleLoggingClient.Dao
{
    public class GCPMQ : IQueueMessenger
    {
        private readonly string _projectId;
        private readonly string _topicId;
        private readonly LogicHelper _LogicHelper;

        private const string MESSAGE_PUBLISHED = "Message Published";

        public GCPMQ(IInitializationInformation initializationInfomormation)
        {
            _projectId = initializationInfomormation.GcpMq.ProjectId;
            _topicId = initializationInfomormation.GcpMq.TopicId;
            _LogicHelper = new LogicHelper(initializationInfomormation);
        }

        public async void SendMessage(byte[] message)
        {
            var publisher = await PublisherClient.CreateAsync(new TopicName(_projectId, _topicId));
            var confirmation = await publisher.PublishAsync(message);

            _LogicHelper.LogToPlatform(MESSAGE_PUBLISHED, confirmation, null, true);
        }
    }
}