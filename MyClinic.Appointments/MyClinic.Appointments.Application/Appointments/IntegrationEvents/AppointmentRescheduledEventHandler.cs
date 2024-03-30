﻿using MyClinic.Common.MessageBus;
using MyClinic.Common.DomainEvents;
using MyClinic.Common.IntegrationsEvents;

using MyClinic.Appointments.Domain.Events;

namespace MyClinic.Appointments.Application.Appointments.IntegrationEvents;

public sealed class AppointmentRescheduledEventHandler : IDomainEventHandler<AppointmentUpdatedEvent>
{
    private readonly IMessageBusProducerService _messageBusService;

    public AppointmentRescheduledEventHandler(IMessageBusProducerService messageBusService)
    {
        _messageBusService = messageBusService;
    }

    public Task Handle(AppointmentUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var sendEmailEvent =
            new SendEmailEvent(
                nameof(AppointmentCreatedEvent),
                notification.Patient.Email,
                "Confirmação de Alteração de Consulta Médica",
                CreateEmailMessage(notification),
                null
            );

        _messageBusService.Publish(nameof(SendEmailEvent), sendEmailEvent);

        _messageBusService.Publish(nameof(AppointmentUpdatedEvent), notification);

        return Task.CompletedTask;
    }

    private static string CreateEmailMessage(AppointmentUpdatedEvent notification)
    {
        var message = $@"
            <!DOCTYPE html>
            <html lang=""pt-br"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Confirmação de Consulta Médica</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 5px;
                        box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.1);
                    }}
                    h1, p {{
                        margin: 0;
                    }}
                    .footer {{
                        margin-top: 20px;
                        padding-top: 20px;
                        border-top: 1px solid #ccc;
                        font-size: 12px;
                        color: #666;
                        text-align: center;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h1>Confirmação de Alteração de Consulta Médica</h1>
                    <p>Prezado(a) {notification.Patient.Name},</p>
                    <p>Gostaríamos de confirmar que o agendamento da sua consulta médica com o Dr. {notification.Doctor.Name} para o procedimento de {notification.Procedure.Name} foi reagendado. Seguem os novos detalhes:</p>
                    <ul>
                        <li><strong>Data:</strong> {notification.Procedure.StartDate:D}</li>
                        <li><strong>Hora de Início:</strong> {notification.Procedure.StartDate:t}</li>
                    </ul>
                    <p>Por favor, chegue com 15 minutos de antecedência. Em caso de qualquer dúvida ou se precisar reagendar, entre em contato conosco o mais breve possível.</p>
                    <p>Agradecemos pela sua confiança e estamos à disposição para qualquer necessidade.</p>
                    <div class=""footer"">
                        <p>Atenciosamente,<br>MyClinic<br>Contato: (99) 9 9999-9999 | Email: myclinic@email.com</p>
                    </div>
                </div>
            </body>
            </html>";

        return message;
    }

}