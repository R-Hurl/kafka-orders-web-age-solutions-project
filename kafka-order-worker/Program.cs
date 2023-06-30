using Confluent.Kafka;
using kafka_project_helper_library.Middleware;
using kafka_order_worker;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("ConsumerConfig", consumerConfig);
builder.Services.AddSingleton(consumerConfig);

builder.Services.RegisterOrdersDatabase();

builder.Services.AddHostedService<OrderWorker>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
