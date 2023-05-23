using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace NyWine.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void PublishMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var conn = factory.CreateConnection();

            using var channel = conn.CreateModel();

            channel.QueueDeclare("wines", durable: true, exclusive: false);

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish("", "wines", body: body);
        }
    }

    /* public class RabbitMQProducer
    {
        private readonly RabbitMQConfiguration _rabbitMQConfig;

        public RabbitMQProducer(RabbitMQConfiguration rabbitMQConfig)
        {
            _rabbitMQConfig = rabbitMQConfig;
        }

        public void PublishMessage<T>(T message, string routingKey)
    where T : class
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.Hostname,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(_rabbitMQConfig.ExchangeName, ExchangeType.Topic, durable: true);

                    var json = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(
                        exchange: _rabbitMQConfig.ExchangeName,
                        routingKey: routingKey,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    } */

}
