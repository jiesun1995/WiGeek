using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WiGeek.Application.Contracts;

namespace WiGeek.Application
{
    public interface IPhysicalSignsService : ICrudAppService<PhysicalSignsDto, int, PagedAndSortedResultRequestDto, CreateUpdatePhysicalSignsDto, CreateUpdatePhysicalSignsDto>
    {
        Task BulkCreatAsync(List<CreateUpdatePhysicalSignsDto> dtos);
    }
}
