namespace WebApp
{
    public static class Constants
    {
        public static class KafkaTopics
        {
            public const string MemberCreated = "daxko-kafka-webapp-member-create";
            public const string MemberUpdated = "daxko-kafka-webapp-member-update";
            public const string MemberDeleted = "daxko-kafka-webapp-member-delete";

            public const string LocationCreated = "daxko-kafka-webapp-location-create";
            public const string LocationUpdated = "daxko-kafka-webapp-location-update";
            public const string LocationDeleted = "daxko-kafka-webapp-location-delete";
        }
    }
}