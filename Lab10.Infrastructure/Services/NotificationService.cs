namespace Lab10.Infrastructure2.Services;

public class NotificationService
{
    public void SendNotification(string user)
        {
            // Este mensaje se imprimirá en la consola de tu IDE (Rider/Visual Studio)
            Console.WriteLine($"[Hangfire Job] Notificación enviada a {user} en {DateTime.Now}");
        }
    
    public void CleanInactiveCourses()
    {
        // Aquí simularías un query como: UPDATE Courses SET IsDeleted = 1 WHERE IsActive = 0
        Console.WriteLine($"[MANTENIMIENTO] [{DateTime.Now}] -> Iniciando limpieza de registros e historial de cursos inactivos...");
        Console.WriteLine($"[MANTENIMIENTO] [{DateTime.Now}] -> ¡Limpieza completada con éxito! Base de datos optimizada.");
    }

}