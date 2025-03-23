using AutoMapper;
using OrderApi.Core.Models;
using OrderReadApi.Endpoints.GetOrderById;
using OrderReadApi.Endpoints.GetOrders;

namespace OrderReadApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, GetOrderByIdResponse>();

            CreateMap<Order, GetOrdersResponseItem>();
        }
    }
}