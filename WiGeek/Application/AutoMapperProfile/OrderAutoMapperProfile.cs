using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.OrderAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<CreateUpdateOrderDto, Order>();
            CreateMap<Order, OrderDto>();
        }
    }
}
