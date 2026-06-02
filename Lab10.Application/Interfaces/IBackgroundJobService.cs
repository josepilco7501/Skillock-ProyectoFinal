namespace Lab10.Application2.Interfaces;

public interface IBackgroundJobService
{
    // Abstraemos el método Enqueue para Fire-and-forget
    void EnqueueNotification(string mensaje);
    
    // Delayed (Diferido)
    void ScheduleNotification(string mensaje, TimeSpan retraso);

    // Recurrente
    void AddOrUpdateRecurringNotification(string jobId, string mensaje, string cronExpression);
}