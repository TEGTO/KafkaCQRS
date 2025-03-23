using AutoMapper;
using OrderApi.Core.Models;
using OrderWriteApi.Endpoints.CreateOrder;
using OrderWriteApi.Endpoints.UpdateOrder;

namespace OrderWriteApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<Order, CreateOrderResponse>();

            CreateMap<UpdateOrderRequest, Order>();
            CreateMap<Order, UpdateOrderResponse>();
        }
    }
}