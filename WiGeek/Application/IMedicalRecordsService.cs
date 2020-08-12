using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MedicalRecordsAggregate;

namespace WiGeek.Application
{
    public interface IMedicalRecordsService: ICrudAppService<MedicalRecordsDto, int, PagedAndSortedResultRequestDto, CreateUpdateMedicalRecordsDto, CreateUpdateMedicalRecordsDto>
    {
        Task BulkCreatAsync(List<CreateUpdateMedicalRecordsDto> dtos);
        void BulkCreat(List<CreateUpdateMedicalRecordsDto> dtos);
    }
}
