using System.Text;
using RabbitMQ.Client;

namespace AndreTurismoApp.Rabbit
{
    public class Producer
    {
        private readonly ConnectionFactory _factory;

        public Producer(ConnectionFactory factory)
        {
            _factory = factory;
        }

        public void Send(string queueName, string message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var bytesMessage = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
        }
    }
}
