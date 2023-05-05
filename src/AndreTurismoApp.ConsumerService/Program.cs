using AndreTurismoApp.ConsumerService.Consumers;
using RabbitMQ.Client;

var factory = new ConnectionFactory() { HostName = "localhost" };

var cityConsumer = new CityConsumer();

using (var connection = factory.CreateConnection())
{
    while (true)
    {
        cityConsumer.Start(connection);
    }
}
