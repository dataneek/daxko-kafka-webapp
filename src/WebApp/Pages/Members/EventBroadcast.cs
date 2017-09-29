namespace WebApp.Pages.Members
{
    using System;
    using System.Threading.Tasks;
    using DotNetCore.CAP;
    using MediatR;
    using Models;

    public class EventBroadcast
        : IAsyncNotificationHandler<MemberEvent.Created>,
          IAsyncNotificationHandler<MemberEvent.Updated>,
          IAsyncNotificationHandler<MemberEvent.Deleted>
    {
        private readonly ICapPublisher publisher;

        public EventBroadcast(ICapPublisher publisher)
        {
            this.publisher = publisher;
        }

        async Task IAsyncNotificationHandler<MemberEvent.Created>.Handle(MemberEvent.Created notification)
        {
            await Publish(Constants.KafkaTopics.MemberCreated, new MemberData(notification.Member));
        }

        async Task IAsyncNotificationHandler<MemberEvent.Updated>.Handle(MemberEvent.Updated notification)
        {
            await Publish(Constants.KafkaTopics.MemberUpdated, new MemberData(notification.Member));
        }

        async Task IAsyncNotificationHandler<MemberEvent.Deleted>.Handle(MemberEvent.Deleted notification)
        {
            await Publish(Constants.KafkaTopics.MemberDeleted, new MemberData(notification.Member));
        }

        private async Task Publish(string topicName, MemberData memberData)
        {
            await publisher.PublishAsync(topicName, memberData);
        }
    }
}