using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.WorkAggregate;

namespace WiGeek.Application
{
    public class WorkService : CrudAppService<Work, WorkDto, int, PagedAndSortedResultRequestDto, CreateUpdateWorkDto, CreateUpdateWorkDto>
    {
        public WorkService(IRepository<Work, int> repository) : base(repository)
        {
        }
    }
}
