using System.Linq;
using kafka_project_dal.Entities;
using kafka_project_dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace kafka_project_dal.Repositories;

public class OrderRepository : IOrderRepository
{
    private const string PlacedStatus = "PLACED";
    private readonly KafkaLabContext _dbContext;

    public OrderRepository(KafkaLabContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrderAsync(Order08 orderToAdd)
    {
        orderToAdd.OrderStatus = PlacedStatus;
        _dbContext.Add(orderToAdd);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(string orderId)
    {
        var orderToDelete = GetOrderById(orderId);
        _dbContext.Remove(orderToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order08> GetOrderById(string orderId)
    {
        var orderIdGuid = Guid.Parse(orderId);
        return await _dbContext.Order08s.Where(order => order.OrderId == orderIdGuid).SingleAsync();
    }

    public async Task<IEnumerable<Order08>> GetOrdersAsync() => await _dbContext.Order08s.ToListAsync();
}
