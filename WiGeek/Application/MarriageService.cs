using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MarriageAggregate;

namespace WiGeek.Application
{
    public class MarriageService : CrudAppService<Marriage, MarriageDto, int, PagedAndSortedResultRequestDto, CreateUpdateMarriageDto, CreateUpdateMarriageDto>
    {
        public MarriageService(IRepository<Marriage, int> repository) : base(repository)
        {
        }
    }
}
