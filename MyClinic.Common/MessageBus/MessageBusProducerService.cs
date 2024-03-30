using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using MyClinic.Common.Options;

namespace MyClinic.Common.MessageBus;

public class MessageBusProducerService : IMessageBusProducerService
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqConfigurationOptions _rabbitMqConfigurationOptions;

    public MessageBusProducerService(IOptions<RabbitMqConfigurationOptions> rabbitMqConfigurationOptions)
    {
        _rabbitMqConfigurationOptions = rabbitMqConfigurationOptions.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqConfigurationOptions.HostName,
            Port = _rabbitMqConfigurationOptions.Port,
            UserName = _rabbitMqConfigurationOptions.UserName,
            Password = _rabbitMqConfigurationOptions.Password
        };
    }

    public void Publish<T>(string queue, T @event)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var body = GetBodyMessage(@event);

        channel.QueueDeclare
            (
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null
            );

        channel.BasicPublish
            (
                exchange: "",
                routingKey: queue,
                basicProperties: null,
                body: body
            );
    }

    private static byte[] GetBodyMessage<T>(T @event)
    {
        var eventMessageJsonString = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(eventMessageJsonString);

        return body;
    }
}