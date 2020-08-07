using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.DepartmentAggregate;

namespace WiGeek.Application
{
    public class DepartmentService : CrudAppService<Department, DepartmentDto, int, PagedAndSortedResultRequestDto, CreateUpdateDepartmentDto, CreateUpdateDepartmentDto>
    {
        public DepartmentService(IRepository<Department, int> repository) : base(repository)
        {
        }
    }
}
