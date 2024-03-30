using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using MyClinic.Common.Options;

using MyClinic.Appointments.Domain.Events;

namespace MyClinic.Appointments.Persistence.Consumers;

public sealed class AppointmentScheduledEventConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqConfigurationOptions _rabbitMqConfigurationOptions;

    private readonly string _queue;

    public AppointmentScheduledEventConsumerService(
        IServiceProvider serviceProvider,
        IOptions<RabbitMqConfigurationOptions> rabbitMqConfiguration)
    {
        _serviceProvider = serviceProvider;
        _rabbitMqConfigurationOptions = rabbitMqConfiguration.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqConfigurationOptions.HostName,
            Port = _rabbitMqConfigurationOptions.Port,
            UserName = _rabbitMqConfigurationOptions.UserName,
            Password = _rabbitMqConfigurationOptions.Password
        };

        _queue = nameof(AppointmentScheduledEvent);

        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare
            (
                queue: _queue,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null
            );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var bytes = eventArgs.Body.ToArray();
            var eventMessage = Encoding.UTF8.GetString(bytes);
            var appointmentScheduledEvent = JsonSerializer.Deserialize<AppointmentScheduledEvent>(eventMessage);

            await UpdateAppointmentStatus(appointmentScheduledEvent);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume
            (
                queue: _queue,
                autoAck: false,
                consumer: consumer
            );

        return Task.CompletedTask;
    }

    private async Task UpdateAppointmentStatus(AppointmentScheduledEvent appointmentScheduledEvent)
    {
        using (var scope = _serviceProvider.CreateAsyncScope())
        {
            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

            await publisher.Publish(appointmentScheduledEvent);
        }
    }
}