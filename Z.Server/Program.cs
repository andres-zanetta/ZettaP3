using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Z.BD.DATA;
using AutoMapper;
using Z.Server.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//nota para mi cambio los nombres de los services porque los tenia con nombre diferente a los del profe
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IPresupuestoRepositorio, PresupuestoRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
