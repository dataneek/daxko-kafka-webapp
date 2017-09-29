namespace WebApp.Pages.Locations
{
    using System;
    using System.Threading.Tasks;
    using DotNetCore.CAP;
    using MediatR;
    using Models;

    public class EventBroadcast
        : IAsyncNotificationHandler<LocationEvent.Created>,
          IAsyncNotificationHandler<LocationEvent.Updated>,
          IAsyncNotificationHandler<LocationEvent.Deleted>
    {
        private readonly ICapPublisher publisher;

        public EventBroadcast(ICapPublisher publisher)
        {
            this.publisher = publisher;
        }

        async Task IAsyncNotificationHandler<LocationEvent.Created>.Handle(LocationEvent.Created notification)
        {
            await Publish(Constants.KafkaTopics.LocationCreated, new LocationData(notification.Location));
        }

        async Task IAsyncNotificationHandler<LocationEvent.Updated>.Handle(LocationEvent.Updated notification)
        {
            await Publish(Constants.KafkaTopics.LocationUpdated, new LocationData(notification.Location));
        }

        async Task IAsyncNotificationHandler<LocationEvent.Deleted>.Handle(LocationEvent.Deleted notification)
        {
            await Publish(Constants.KafkaTopics.LocationDeleted, new LocationData(notification.Location));
        }

        private async Task Publish(string topicName, LocationData locationData)
        {
            await publisher.PublishAsync(topicName, locationData);
        }
    }
}