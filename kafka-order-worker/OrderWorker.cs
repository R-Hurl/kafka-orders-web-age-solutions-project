using Confluent.Kafka;
using kafka_project_dal.Entities;
using kafka_project_dal.Repositories.Interfaces;
using kafka_project_helper_library.Models;
using Newtonsoft.Json;

namespace kafka_order_worker;

public class OrderWorker : BackgroundService
{
    private const string TopicName = "orders";
    private readonly ILogger<OrderWorker> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public OrderWorker(ConsumerConfig consumerConfig, ILogger<OrderWorker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        _consumer.Subscribe(TopicName);
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OrderWorker started...");

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var message = _consumer.Consume();
            var order = JsonConvert.DeserializeObject<Order08>(message.Message.Value);

            _logger.LogInformation($"Consumed PENDING order, {message.Message.Value}");

            await orderRepository.AddOrderAsync(order);

        }

        return;
    }

}