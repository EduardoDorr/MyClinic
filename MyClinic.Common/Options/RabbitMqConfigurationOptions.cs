namespace MyClinic.Common.Options;

public sealed class RabbitMqConfigurationOptions
{
    public required string HostName { get; set; }
    public required int Port { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}