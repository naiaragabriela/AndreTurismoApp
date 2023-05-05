using System.Text;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AndreTurismoApp.ConsumerService.Consumers
{
    public class CityConsumer
    {
        private const string QUEUE_NAME = "city";

        public void Start(IConnection connection)
        {
            try
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME,
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var returnMessage = Encoding.UTF8.GetString(body);
                        var city = JsonConvert.DeserializeObject<City>(returnMessage);
                        var repository = new CityRepository();
                        repository.Add(city);
                    };

                    channel.BasicConsume(queue: QUEUE_NAME,
                                         autoAck: true,
                                         consumer: consumer);

                    Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
