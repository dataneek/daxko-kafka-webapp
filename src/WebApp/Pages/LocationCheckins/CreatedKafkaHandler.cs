namespace WebApp.Pages.LocationCheckins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;
    using Common;
    using DotNetCore.CAP;

    public class CreatedKafkaHandler : IAsyncNotificationHandler<LocationCheckinEvent.Created>
    {
        private readonly ICapPublisher publisher;

        public CreatedKafkaHandler(ICapPublisher publisher)
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