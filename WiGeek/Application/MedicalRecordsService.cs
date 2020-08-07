using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using WiGeek.Application.Contracts;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;

namespace WiGeek.Application
{
    public class MedicalRecordsService : CrudAppService<MedicalRecords, MedicalRecordsDto, int, PagedAndSortedResultRequestDto, CreateUpdateMedicalRecordsDto, CreateUpdateMedicalRecordsDto>
    {
        private readonly IRepository<Work, int> _workRepository;
        private readonly IRepository<Marriage, int> _marriageRepository;
        private readonly IRepository<Diagnosis, int> _diagnosisRepository;
        private readonly IRepository<Department, int> _departmentRepository;
        private readonly IRepository<Ward, int> _wardRepository;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<PhysicalSigns, int> _physicalSignsRepository;

        public MedicalRecordsService(IRepository<Work, int> workRepository
            , IRepository<Marriage, int> marriageRepository
            , IRepository<MedicalRecords, int> repository
            , IRepository<Diagnosis, int> diagnosisRepository
            , IRepository<Order, int> orderRepository
            , IRepository<PhysicalSigns, int> physicalSignsRepository
            , IRepository<Department, int> departmentRepository
            , IRepository<Ward, int> wardRepository) : base(repository)
        {
            _workRepository = workRepository;
            _marriageRepository = marriageRepository;
            _diagnosisRepository = diagnosisRepository;
            _departmentRepository = departmentRepository;
            _wardRepository = wardRepository;
            _orderRepository = orderRepository;
            _physicalSignsRepository = physicalSignsRepository;
        }

        public override async Task<MedicalRecordsDto> CreateAsync(CreateUpdateMedicalRecordsDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(input);
            SetIdForGuids(entity);

            entity.Work = _workRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosWardId);
            entity.Department = _departmentRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosDepartmentId);
            entity.Marriage = _marriageRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosMarriageId);
            entity.Ward = _wardRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosWardId);
            entity.Diagnosis = _diagnosisRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.DiagnosisType == input.HosDiagnosisName);
            entity.PhysicalSigns = _physicalSignsRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosPhysicalSignsId);
            var orders = _orderRepository.Where(x => x.HospitalId == input.HospitalId && input.HosOrderIds.Contains(x.HospitalCode)).ToList();

            if (entity.DiagnosisId == null)
                throw new Exception("无诊断数据");
            if (entity.PhysicalSignsId == null)
                throw new Exception("无体征数据");
            if (orders == null || orders.Count <= 0)
                throw new Exception("无医嘱数据");

            await Repository.InsertAsync(entity, autoSave: true);
            
            return ObjectMapper.Map<MedicalRecords, MedicalRecordsDto>(entity);
        }
    }
}
