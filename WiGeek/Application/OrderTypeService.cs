using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;

namespace WiGeek.Application
{
    public class OrderTypeService : CrudAppService<OrderType, OrderTypeDto, int, PagedAndSortedResultRequestDto, CreateUpdateOrderTypeDto, CreateUpdateOrderTypeDto>
    {
        public OrderTypeService(IRepository<OrderType, int> repository) : base(repository)
        {
        }
    }
}
