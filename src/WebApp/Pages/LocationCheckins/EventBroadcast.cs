namespace WebApp.Pages.LocationCheckins
{
    using System;
    using System.Threading.Tasks;
    using DotNetCore.CAP;
    using MediatR;
    using Models;

    public class EventBroadcast : IAsyncNotificationHandler<LocationCheckinEvent.Created>
    {
        private readonly ICapPublisher publisher;

        public EventBroadcast(ICapPublisher publisher)
        {
            this.publisher = publisher;
        }

        async Task IAsyncNotificationHandler<LocationCheckinEvent.Created>.Handle(LocationCheckinEvent.Created notification)
        {
            await publisher.PublishAsync(
                Constants.KafkaTopics.LocationCheckinCreated, 
                new LocationCheckinData(notification.LocationCheckin));
        }
    }
}