using kafka_project_dal.Entities;

namespace kafka_project_dal.Repositories.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order08 orderToAdd);
    Task<IEnumerable<Order08>> GetOrdersAsync();
    Task<Order08> GetOrderById(string orderId);
    Task DeleteOrderAsync(string orderId);
}