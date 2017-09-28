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
            var content = JsonConvert.SerializeObject(new Data(member));
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

        public class Data
        {
            public Data(Member member)
            {
                RowId = member.RowId;
                FirstName = member.FirstName;
                LastName = member.LastName;
                Gender = member.Gender;
                Phone = member.Phone;
                Email = member.Email;
                Birthdate = member.Birthdate;
            }

            public Guid RowId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime Birthdate { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
    }
}