using Hangfire;
using Lab10.Configuration;
using Lab10.Infrastructure2.Services;

var builder = WebApplication.CreateBuilder(args);

// Centralizamos 
builder.Services.AddApplicationServices(builder.Configuration);

// 2. AGREGAR SERVICIOS DE HANGFIRE AQUÍ
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configuración del pipeline HTTP (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middlewares obligatorios de seguridad
app.UseAuthentication(); 
app.UseAuthorization();  

// 3. AGREGAR DASHBOARD DE HANGFIRE AQUÍ (Siempre después de UseAuthorization)
app.UseHangfireDashboard("/hangfire");

app.MapControllers(); 

// === ACTIVIDAD ADICIONAL: JOB RECURRENTE DE MANTENIMIENTO ===
using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    
    recurringJobManager.AddOrUpdate<NotificationService>(
        "mantenimiento-limpieza-bd", 
        service => service.CleanInactiveCourses(), // Llamamos a nuestro nuevo método
        "0 1 * * *" // Se ejecutará automáticamente TODOS los días a la 1:00 AM
    );
}
app.Run();