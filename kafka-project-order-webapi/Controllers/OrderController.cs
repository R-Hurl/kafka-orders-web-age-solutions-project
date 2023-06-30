using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using kafka_project_dal.Repositories.Interfaces;
using kafka_project_helper_library.Models;
using AutoMapper;
using kafka_project_dal.Entities;

namespace kafka_project_order_webapi.Controllers
{
    [ApiController]
    [Route("/api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ProducerConfig _producerConfig;
        private readonly ILogger<OrderController> _logger;
        private readonly IProducer<string, string> _producer;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        private const string PendingStatus = "PENDING";

        public OrderController(ProducerConfig producerConfig, ILogger<OrderController> logger, IOrderRepository orderRepository, IMapper mapper)
        {
            _producerConfig = producerConfig;
            _logger = logger;
            _producer = new ProducerBuilder<string, string>(_producerConfig).Build();
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetOrders()
        {
            var orders = await _orderRepository.GetOrdersAsync();

            var ordersToReturn = new List<OrderModel>();
            foreach (var order in orders)
            {
                ordersToReturn.Add(_mapper.Map<Order08, OrderModel>(order));
            }

            return ordersToReturn;
        }

        [HttpGet("{id}")]
        public OrderModel GetOrderById(string id)
        {
            // TODO: Modify this code to use Entity Framework and the scaffolded/auto-generated model class for the Order table

            var sampleOrders = new List<OrderModel> {
                new OrderModel { OrderId = "5a59bab5-5e2e-46d6-a6ef-97cb446ec9f9", CustomerName = "Bob Smith", OrderDescription = "Mobile Phone", OrderDate = new DateTime(2022, 01, 02, 10, 10, 00), OrderAmount = 699.95F, OrderStatus = "PENDING"},
                new OrderModel { OrderId = "5a59bab5-5e2e-46d6-a6ef-97cb446ec9f8", CustomerName = "Drew Smith", OrderDescription = "PS5", OrderDate = new DateTime(2022, 02, 03, 05, 06, 00), OrderAmount = 299.95F, OrderStatus = "PLACED"},
                new OrderModel { OrderId = "5a59bab5-5e2e-46d6-a6ef-97cb446ec9f7", CustomerName = "Bob Smith", OrderDescription = "Laptop", OrderDate = new DateTime(2022, 03, 04, 09, 11, 00), OrderAmount = 1699.95F, OrderStatus = "SHIPPED"}
            };

            var order = sampleOrders.Where(order => order.OrderId == id).SingleOrDefault();

            return order;
        }

        [HttpPost]
        public async Task<string> PostOrder(OrderModel model)
        {
            try
            {
                // TODO: Modify this code to add Kafka support.
                model.OrderStatus = PendingStatus;
                var message = new Message<string, string>
                {
                    Key = model.OrderId,
                    Value = JsonConvert.SerializeObject(model)
                };

                var producerResponse = await _producer.ProduceAsync("orders", message);

                _logger.LogInformation($"Received Order with Key: {producerResponse.Message.Key}, and Value: {producerResponse.Message.Value}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Occurred Placing Order to Kafka Topic");
                return "Unable to place order at this time... Sorry";
            }
            // NOTE: This DELETE method in the starter project is there as placeholder. It works but since we have in-memory data, the data isn't really deleted.

            return $"OrderId: {model.OrderId} received!";
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteOrderById(string id)
        {
            
            _logger.LogInformation($"Attempting to delete order {id}");

            try
            {
                await _orderRepository.DeleteOrderAsync(id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred deleting order from the database.");
                return false;
            }

            return true;
        }

    }
}
