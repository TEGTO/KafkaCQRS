namespace OrderApi.Common
{
    public static class ConfigurationKeys
    {
        public static string KafkaBootstrapServers => "Kafka:BootstrapServers";
        public static string OrderTopic => "Kafka:OrderTopic";
    }
}
