using Hangfire;
using Lab10.Application2.Interfaces;
namespace Lab10.Infrastructure2.Services;

public class BackgroundJobService : IBackgroundJobService
{
    private readonly NotificationService _notificationService;

    public BackgroundJobService(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void EnqueueNotification(string mensaje)
    {
        // Aquí sí podemos usar Hangfire de manera segura
        BackgroundJob.Enqueue<NotificationService>(service => 
            service.SendNotification(mensaje));
    }
    
    public void ScheduleNotification(string mensaje, TimeSpan retraso)
    {
        // Uso de BackgroundJob.Schedule para tareas programadas a futuro
        BackgroundJob.Schedule<NotificationService>(service => 
            service.SendNotification(mensaje), retraso);
    }

    public void AddOrUpdateRecurringNotification(string jobId, string mensaje, string cronExpression)
    {
        // Uso de RecurringJob para tareas que se repiten con un patrón Cron
        RecurringJob.AddOrUpdate<NotificationService>(jobId, service => 
            service.SendNotification(mensaje), cronExpression);
    }
}