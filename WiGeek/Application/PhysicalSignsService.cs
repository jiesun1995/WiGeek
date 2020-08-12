using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Infrastructure.Repositories;

namespace WiGeek.Application
{
    public class PhysicalSignsService : CrudAppService<PhysicalSigns, PhysicalSignsDto, int, PagedAndSortedResultRequestDto, CreateUpdatePhysicalSignsDto, CreateUpdatePhysicalSignsDto>, IPhysicalSignsService
    {
        private readonly IRepository<MedicalRecords, int> _medicalRecordsRepository;
        private readonly IPhysicalSignsDapperRepository _physicalSignsDapperRepository;

        public PhysicalSignsService(IPhysicalSignsDapperRepository physicalSignsDapperRepository, IRepository<PhysicalSigns, int> repository,IRepository<MedicalRecords, int> medicalRecordsRepository)
            : base(repository)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
            _physicalSignsDapperRepository = physicalSignsDapperRepository;
        }

        private PhysicalSigns Map(CreateUpdatePhysicalSignsDto dto)
        {
            return new PhysicalSigns
            {
                HospitalCode= dto.HospitalCode,
                HospitalId=dto.HospitalId,
                Temperature=dto.Temperature,
                Breathe=dto.Breathe,
                HeartRate=dto.HeartRate,
                //MedicalRecordsId=dto.MedicalRecordsId,
            };
        }

        public async Task BulkCreatAsync(List<CreateUpdatePhysicalSignsDto> dtos)
        {
            ConcurrentBag<PhysicalSigns> physicalSigns = new ConcurrentBag<PhysicalSigns>();

            //var list = dtos.Select(y => y.HosMedicalRecordsId).Distinct();
            //var medicalRecords = await _medicalRecordsRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(y => y.MedicalRecordsId).Distinct().Contains(x.HospitalCode)).ToListAsync();           

            Parallel.ForEach(dtos, dto =>
            {
                //medicalRecord.Work = works.FirstOrDefault(x => x.HospitalCode == medicalRecord.WardId);
                //var entity = ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(dto);
                var entity = Map(dto);
                //entity.MedicalRecords = medicalRecords.FirstOrDefault(x => x.HospitalCode == dto.MedicalRecordsId);

                physicalSigns.Add(entity);
            });
            //await _physicalSignsDapperRepository.BulkCreatAsync(physicalSigns.ToList());
            await Repository.GetDbContext().BulkInsertAsync(physicalSigns.ToList());
        }
    }
}
