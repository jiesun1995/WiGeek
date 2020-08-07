using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.ValueObject;

namespace WiGeek.Application
{
    public class PhysicalSignsService : CrudAppService<PhysicalSigns, PhysicalSignsDto, int, PagedAndSortedResultRequestDto, CreateUpdatePhysicalSignsDto, CreateUpdatePhysicalSignsDto>
    {
        public PhysicalSignsService(IRepository<PhysicalSigns, int> repository) : base(repository)
        {
        }
    }
}
