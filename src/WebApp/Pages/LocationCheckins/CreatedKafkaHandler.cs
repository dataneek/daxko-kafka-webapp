namespace WebApp.Pages.LocationCheckins
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

    public class CreatedKafkaHandler : INotificationHandler<LocationCheckinEvent.Created>
    {
        private readonly KafkaSettings kafkaSettings;

        public CreatedKafkaHandler(KafkaSettings kafkaSettings)
        {
            this.kafkaSettings = kafkaSettings;
        }

        void INotificationHandler<LocationCheckinEvent.Created>.Handle(LocationCheckinEvent.Created notification)
        {
            try
            {
                HandleInternal(notification.LocationCheckin);
            }
            catch(Exception) { }
        }

        private void HandleInternal(LocationCheckin t)
        {
            var content = JsonConvert.SerializeObject(new LocationCheckinData(t));
            var config = new Dictionary<string, object> 
            { 
                { "bootstrap.servers", kafkaSettings.BrokerList } 
            };

            using (var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8)))
            {
                var deliveryReport = 
                    producer.ProduceAsync(
                        Constants.KafkaTopics.LocationCheckinCreated, 
                        null, 
                        content);

                var result = deliveryReport.Result; 
            }
        }
    }
}