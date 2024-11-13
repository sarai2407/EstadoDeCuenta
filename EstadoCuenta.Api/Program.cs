using Microsoft.EntityFrameworkCore;
using EstadoCuenta.Data;
using EstadoCuenta.Api.Interfaces;
using EstadoCuenta.Api.UnitOfWork;
using EstadoCuenta.Api.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EstadoCuentaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Registrar UnitOfWork y repositorios
#region
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ITarjetaRepositorio, TarjetaRepositorio>();
builder.Services.AddScoped<ITransaccionRepositorio, TransaccionRepositorio>();
builder.Services.AddScoped<ITipoTransaccionRepositorio, TipoTransaccionRepositorio>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

/*  Descomentar para crear la Base de datos con sus tablas 
using (var scope = app.Services.CreateScope())
{
    var Context = scope.ServiceProvider.GetRequiredService<EstadoCuentaContext>();
    Context.Database.Migrate();
}
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
