using AutoMapper;
using kafka_project_dal.Entities;
using kafka_project_helper_library.Models;

namespace kafka_project_order_webapi.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order08, OrderModel>();
    }
}