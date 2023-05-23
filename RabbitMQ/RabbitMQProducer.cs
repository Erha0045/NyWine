using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using NyWine.RabbitMQ;

namespace NyWine.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly RabbitMQConfiguration _rabbitMQConfig;

        public RabbitMQProducer(RabbitMQConfiguration rabbitMQConfig)
        {
            _rabbitMQConfig = rabbitMQConfig;
        }

        public void PublishMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {

                HostName = _rabbitMQConfig.Hostname,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password,
                //VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            channel.QueueDeclare("wineQueue", durable: false, exclusive: false, autoDelete: true);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "wineQueue", body: body);
        }
    }
}
