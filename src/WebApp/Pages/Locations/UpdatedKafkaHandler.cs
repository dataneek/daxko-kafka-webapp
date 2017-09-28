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

    public class UpdatedKafkaHandler : INotificationHandler<LocationEvent.Updated>
    {
        private readonly KafkaSettings kafkaSettings;

        public UpdatedKafkaHandler(KafkaSettings kafkaSettings)
        {
            this.kafkaSettings = kafkaSettings;
        }

        void INotificationHandler<LocationEvent.Updated>.Handle(LocationEvent.Updated notification)
        {
            try
            {
                HandleInternal(notification.Location);
            }
            catch(Exception) { }
        }

        private void HandleInternal(Location Location)
        {
            var content = JsonConvert.SerializeObject(new LocationData(Location));
            var config = new Dictionary<string, object> 
            { 
                { "bootstrap.servers", kafkaSettings.BrokerList } 
            };

            using (var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8)))
            {
                var deliveryReport = 
                    producer.ProduceAsync(
                        Constants.KafkaTopics.LocationUpdated, 
                        null, 
                        content);

                var result = deliveryReport.Result; 
            }
        }
    }
}