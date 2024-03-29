namespace MyClinic.Common.MessageBus;

public interface IMessageBusProducerService
{
    void Publish<T>(string queue, T @event);
}