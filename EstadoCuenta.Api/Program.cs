using Microsoft.EntityFrameworkCore;
using EstadoCuenta.Data;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.UnitOfWork;
using EstadoCuenta.Api.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using EstadoCuenta.Api.Middleware;
using EstadoCuenta.Api.iTextSharp;
using OfficeOpenXml;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen
              .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
              .AllowAnyHeader(); // Permite cualquier encabezado
    });
});

// Add services to the container.
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EstadoCuentaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar UnitOfWork y repositorios
#region
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ITarjetaRepositorio, TarjetaRepositorio>();
builder.Services.AddScoped<ITransaccionRepositorio, TransaccionRepositorio>();
builder.Services.AddScoped<ITipoTransaccionRepositorio, TipoTransaccionRepositorio>();
builder.Services.AddScoped<IGenerarPdfs, GenerarPdfs>();


#endregion

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configurar Health Checks en el proyecto API
builder.Services.AddHealthChecks()
    .AddDbContextCheck<EstadoCuentaContext>(name: "EstadoCuentaDB") // Health Check de la base de datos
    .AddCheck("API Self-Check", () => HealthCheckResult.Healthy());

var app = builder.Build();

/*  Descomentar para crear las tablas de la base de datos EstadoCuentaDB
using (var scope = app.Services.CreateScope())
{
    var Context = scope.ServiceProvider.GetRequiredService<EstadoCuentaContext>();
    Context.Database.Migrate();
}
*/

// Configure the HTTP request pipeline.

// Usar el middleware global de excepciones

// 2. Aplicar la política de CORS en el pipeline de la aplicación
app.UseCors("AllowAllOrigins");  // Aplica la política "AllowAllOrigins"

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Mapear el endpoint de Health Checks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                exception = entry.Value.Exception?.Message,
                duration = entry.Value.Duration.ToString()
            })
        };
        await context.Response.WriteAsJsonAsync(response);
    }
});

app.Run();
