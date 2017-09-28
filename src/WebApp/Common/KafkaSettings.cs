namespace WebApp.Common
{
    public class KafkaSettings
    {
        public KafkaSettings(string zookeeper, string brokerList)
        {
            Zookeeper = zookeeper;
            BrokerList = brokerList;
        }
        
        public string Zookeeper { get; private set; }
        public string BrokerList { get; private set; }
    }
}