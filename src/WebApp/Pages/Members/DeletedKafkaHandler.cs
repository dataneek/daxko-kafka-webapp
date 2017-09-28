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

    public class DeletedKafkaHandler : INotificationHandler<MemberEvent.Deleted>
    {
        private readonly KafkaSettings kafkaSettings;

        public DeletedKafkaHandler(KafkaSettings kafkaSettings)
        {
            this.kafkaSettings = kafkaSettings;
        }

        void INotificationHandler<MemberEvent.Deleted>.Handle(MemberEvent.Deleted notification)
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
                        Constants.KafkaTopics.MemberDeleted, 
                        null, 
                        content);

                var result = deliveryReport.Result; 
            }
        }
    }
}