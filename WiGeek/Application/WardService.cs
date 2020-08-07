using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.WardAggregate;

namespace WiGeek.Application
{
    public class WardService : CrudAppService<Ward, WardDto, int, PagedAndSortedResultRequestDto, CreateUpdateWardDto, CreateUpdateWardDto>
    {
        public WardService(IRepository<Ward, int> repository) : base(repository)
        {
        }
    }
}
