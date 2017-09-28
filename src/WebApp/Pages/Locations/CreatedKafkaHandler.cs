namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;
    using Newtonsoft.Json;
    using Confluent.Kafka;
    using Confluent.Kafka.Serialization;
    using Common;

    public class CreatedKafkaHandler : INotificationHandler<LocationEvent.Created>
    {
        private readonly KafkaSettings kafkaSettings;

        public CreatedKafkaHandler(KafkaSettings kafkaSettings)
        {
            this.kafkaSettings = kafkaSettings;
        }

        void INotificationHandler<LocationEvent.Created>.Handle(LocationEvent.Created notification)
        {
            try
            {
                HandleInternal(notification.Location);
            }
            catch(Exception) { }
        }

        private void HandleInternal(Location t)
        {
            var content = JsonConvert.SerializeObject(new LocationData(t));
            var config = new Dictionary<string, object> 
            { 
                { "bootstrap.servers", kafkaSettings.BrokerList } 
            };

            using (var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8)))
            {
                var deliveryReport = 
                    producer.ProduceAsync(
                        Constants.KafkaTopics.LocationCreated, 
                        null, 
                        content);

                var result = deliveryReport.Result; 
            }
        }
    }
}