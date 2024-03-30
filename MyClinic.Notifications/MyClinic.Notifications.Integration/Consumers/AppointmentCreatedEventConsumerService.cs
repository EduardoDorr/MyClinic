using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using MyClinic.Common.Options;
using MyClinic.Common.MessageBus;

using MyClinic.Notifications.Integration.GoogleCalendar;
using MyClinic.Notifications.Integration.IntegrationEvents;

namespace MyClinic.Notifications.Integration.Consumers;

public sealed class AppointmentCreatedEventConsumerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqConfigurationOptions _rabbitMqConfigurationOptions;

    private readonly string _queue;

    public AppointmentCreatedEventConsumerService(
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

        _queue = nameof(AppointmentCreatedEvent);

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
            var appointmentCreatedEvent = JsonSerializer.Deserialize<AppointmentCreatedEvent>(eventMessage);

            await CreateEvent(appointmentCreatedEvent);

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

    private async Task CreateEvent(AppointmentCreatedEvent appointmentCreatedEvent)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        
        var googleCalendarService = scope.ServiceProvider.GetRequiredService<IGoogleCalendarService>();
        var messageBusService = scope.ServiceProvider.GetRequiredService<IMessageBusProducerService>();

        var attendees = new List<string>
            {
                appointmentCreatedEvent.Patient.Email,
                appointmentCreatedEvent.Doctor.Email
            };

        var eventRequest =
            new GoogleCalendarEventRequest(
                $"Agendamento de consulta: {appointmentCreatedEvent.Procedure.Name}",
                $"Agendamento de consulta: {appointmentCreatedEvent.Procedure.Name}",
                appointmentCreatedEvent.Procedure.StartDate,
                appointmentCreatedEvent.Procedure.EndDate,
                attendees);

        var eventCreated = await googleCalendarService.CreateEvent(eventRequest);

        var appointmentScheduledEvent = new AppointmentScheduledEvent(appointmentCreatedEvent.AppointmentId, eventCreated.Id);

        messageBusService.Publish(nameof(AppointmentScheduledEvent), appointmentScheduledEvent);
    }
}