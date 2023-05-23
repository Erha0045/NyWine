namespace NyWine.RabbitMQ
{
    public interface IMessageProducer
    {
        public void PublishMessage<T>(T message);
    }
}