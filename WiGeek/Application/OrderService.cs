using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.OrderAggregate;

namespace WiGeek.Application
{
    public class OrderService : CrudAppService<Order, OrderDto, int, PagedAndSortedResultRequestDto, CreateUpdateOrderDto, CreateUpdateOrderDto>
    {
        public OrderService(IRepository<Order, int> repository) : base(repository)
        {
        }
    }
}
