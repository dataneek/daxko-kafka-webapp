namespace WebApp.Pages.Members
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

    public class UpdatedKafkaHandler : INotificationHandler<MemberEvent.Updated>
    {
        private readonly KafkaSettings kafkaSettings;

        public UpdatedKafkaHandler(KafkaSettings kafkaSettings)
        {
            this.kafkaSettings = kafkaSettings;
        }

        void INotificationHandler<MemberEvent.Updated>.Handle(MemberEvent.Updated notification)
        {
            try
            {
                HandleInternal(notification.Member);
            }
            catch(Exception) { }
        }

        private void HandleInternal(Member member)
        {
            var content = JsonConvert.SerializeObject(new MemberData(member));
            var config = new Dictionary<string, object> 
            { 
                { "bootstrap.servers", kafkaSettings.BrokerList } 
            };

            using (var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8)))
            {
                var deliveryReport = 
                    producer.ProduceAsync(
                        Constants.KafkaTopics.MemberUpdated, 
                        null, 
                        content);

                var result = deliveryReport.Result; 
            }
        }
    }
}