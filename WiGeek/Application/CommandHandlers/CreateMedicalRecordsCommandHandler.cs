using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.ObjectMapping;
using WiGeek.Application.Contracts;
using WiGeek.Application.Contracts.Report;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Startup;

namespace WiGeek.Application.CommandHandlers
{
    public class CreateMedicalRecordsCommandHandler : ILocalEventHandler<EntityCreatedEventData<MedicalRecords>>
    {
        private readonly IDistributedCache<List<DReportDto>> _dReportcache;
        private readonly static object _object = new object();
        private readonly Volo.Abp.ObjectMapping.IObjectMapper _objectMapper;

        public CreateMedicalRecordsCommandHandler(IDistributedCache<List<DReportDto>> dReportcache, Volo.Abp.ObjectMapping.IObjectMapper objectMapper)
        {
            _dReportcache = dReportcache;
            _objectMapper = objectMapper;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<MedicalRecords> eventData)
        {
            _ = Task.Factory.StartNew(() =>
               {
                   if (eventData.Entity.Age > 10 && eventData.Entity.Age < 40 && eventData.Entity.Marriage.Name == "已婚" && eventData.Entity.Department.Name == "呼吸科")
                   {
                       lock (_object)
                       {
                           var dReportDtos = _dReportcache.GetOrAdd(nameof(DReportDto), () =>
                           {
                               return new List<DReportDto>();
                           });
                           var dReportDto = _objectMapper.Map<MedicalRecords, DReportDto>(eventData.Entity);
                           dReportDtos.Add(dReportDto);
                           _dReportcache.Set(nameof(DReportDto), dReportDtos);
                       }
                   }
               });
            await Task.CompletedTask;
        }
    }
}
