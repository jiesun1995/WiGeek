using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WiGeek.Application.Contracts;

namespace WiGeek.Application
{
    public interface IOrderService: ICrudAppService< OrderDto, int, PagedAndSortedResultRequestDto, CreateUpdateOrderDto, CreateUpdateOrderDto>
    {
        Task BulkCreatAsync(List<CreateUpdateOrderDto> dtos);
    }
}
