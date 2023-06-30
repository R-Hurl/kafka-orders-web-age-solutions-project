using Confluent.Kafka;
using kafka_project_dal;
using kafka_project_dal.Repositories;
using kafka_project_dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_project_helper_library.Middleware;

public static class ServiceCollectionExtensions
{
    private static readonly string CONNECTION_STRING = "Server=tcp:azuresql-server-706703.database.windows.net,1433;Initial Catalog=KafkaLab;Persist Security Info=False;User ID=dba;Password=Passw0rD-706703;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public static IServiceCollection RegisterOrdersDatabase(this IServiceCollection services)
    {
        services.AddDbContext<KafkaLabContext>(options =>
        {
            options.UseSqlServer(CONNECTION_STRING);
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        
        return services;
    }

    public static IServiceCollection RegisterKafkaConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var consumerConfig = new ConsumerConfig();
        configuration.GetSection("ConsumerConfig").Bind(consumerConfig);
        services.AddSingleton(consumerConfig);

        return services;
    }

    public static IServiceCollection RegisterKafkaProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig();
        configuration.GetSection("ProducerConfig").Bind(producerConfig);
        services.AddSingleton(producerConfig);

        return services;
    }
}