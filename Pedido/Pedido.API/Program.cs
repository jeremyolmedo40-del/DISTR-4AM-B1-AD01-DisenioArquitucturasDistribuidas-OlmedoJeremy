using Microsoft.EntityFrameworkCore;
using Pedido.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de la base de datos
builder.Services.AddDbContext<PedidosDBContext>(options =>
    options.UseSqlServer("Data Source=JERE\\SQLSERVER01;Initial Catalog=PedidosDB;Integrated Security=True;Encrypt=False;"));

// Add services to the container.
builder.Services.AddControllers();

// 2. CONFIGURACIÓN DE SWAGGER (Interfaz visual)
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // 3. ACTIVAR LA INTERFAZ DE SWAGGER EN MODO DESARROLLO
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();