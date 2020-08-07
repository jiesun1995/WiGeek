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
    public class DiagnosisService : CrudAppService<Diagnosis, DiagnosisDto, int, PagedAndSortedResultRequestDto, CreateUpdateDiagnosisDto, CreateUpdateDiagnosisDto>
    {
        public DiagnosisService(IRepository<Diagnosis, int> repository) : base(repository)
        {
        }
    }
}
