using Lab10.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Centralizamos 
builder.Services.AddApplicationServices(builder.Configuration);

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

app.MapControllers(); 
app.Run();