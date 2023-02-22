using System.Text.Json.Serialization;
using API_PedroPinturas.Data;
using API_PedroPinturas.DataAccess.Servicios;
using API_PedroPinturas.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// """DATABASE AND DEPENDENCY INJECTION""" //
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<PedroPinturasDb>(options =>
    options.UseNpgsql(connectionString));
//SOLUCCIONAR ERROR CICLO DE OBJETO
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<RespositoryAsync<Color>, RespositoryAsync<Color>>();
builder.Services.AddScoped<RespositoryAsync<Usuario>, RespositoryAsync<Usuario>>();
builder.Services.AddScoped<RespositoryAsync<Pedido>, RespositoryAsync<Pedido>>();
builder.Services.AddScoped<RespositoryAsync<Compra>, RespositoryAsync<Compra>>();
builder.Services.AddScoped<RespositoryAsync<Producto>, RespositoryAsync<Producto>>();
// """DATABASE AND DEPENDENCY INJECTION""" //

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Urls.Add("http://+:5062"); // 5062 y todas las direcciones

app.Run();
