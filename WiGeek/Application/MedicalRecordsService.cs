using AutoMapper;
using EFCore.BulkExtensions;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using Volo.Abp.ObjectMapping;
using WiGeek.Application.Contracts;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;
using WiGeek.Infrastructure.Repositories;
using WiGeek.Startup;

namespace WiGeek.Application
{
    public class MedicalRecordsService : CrudAppService<MedicalRecords, MedicalRecordsDto, int, PagedAndSortedResultRequestDto, CreateUpdateMedicalRecordsDto, CreateUpdateMedicalRecordsDto> , IMedicalRecordsService
    {
        private readonly IRepository<Work, int> _workRepository;
        private readonly IRepository<Marriage, int> _marriageRepository;
        private readonly IRepository<Diagnosis, int> _diagnosisRepository;
        private readonly IRepository<Department, int> _departmentRepository;
        private readonly IRepository<Ward, int> _wardRepository;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<PhysicalSigns, int> _physicalSignsRepository;
        private readonly IMedicalRecordsDapperRepository _medicalRecordsDapperRepository;
        private readonly string _connectionString;
        public MedicalRecordsService(IRepository<Work, int> workRepository
            , IRepository<Marriage, int> marriageRepository
            , IRepository<MedicalRecords, int> repository
            , IRepository<Diagnosis, int> diagnosisRepository
            , IRepository<Order, int> orderRepository
            , IRepository<PhysicalSigns, int> physicalSignsRepository
            , IRepository<Department, int> departmentRepository
            , IMedicalRecordsDapperRepository medicalRecordsDapperRepository
            , IConfiguration configuration
            , IRepository<Ward, int> wardRepository) : base(repository)
        {
            _workRepository = workRepository;
            _marriageRepository = marriageRepository;
            _diagnosisRepository = diagnosisRepository;
            _departmentRepository = departmentRepository;
            _wardRepository = wardRepository;
            _orderRepository = orderRepository;
            _physicalSignsRepository = physicalSignsRepository;
            _medicalRecordsDapperRepository = medicalRecordsDapperRepository;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public override async Task<MedicalRecordsDto> CreateAsync(CreateUpdateMedicalRecordsDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = base.ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(input);
            SetIdForGuids(entity);

            entity.Work = _workRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosWardId);
            entity.Department = _departmentRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosDepartmentId);
            entity.Marriage = _marriageRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosMarriageId);
            entity.Ward = _wardRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosWardId);
            entity.Diagnosis = _diagnosisRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.DiagnosisType == input.HosDiagnosisName);
            //entity.PhysicalSigns = _physicalSignsRepository.FirstOrDefault(x => x.HospitalId == input.HospitalId && x.HospitalCode == input.HosPhysicalSignsId);
            //var orders = _orderRepository.Where(x => x.HospitalId == input.HospitalId && input.HosOrderIds.Contains(x.HospitalCode)).ToList();

            //if (entity.DiagnosisId == null)
            //    throw new Exception("无诊断数据");
            //if (entity.PhysicalSignsId == null)
            //    throw new Exception("无体征数据");
            //if (orders == null || orders.Count <= 0)
            //    throw new Exception("无医嘱数据");

            await Repository.InsertAsync(entity, autoSave: true);
            
            return ObjectMapper.Map<MedicalRecords, MedicalRecordsDto>(entity);
        }

        private MedicalRecords Map(CreateUpdateMedicalRecordsDto dto)
        {
            return new MedicalRecords
            {
                Sex=dto.Sex,
                Add=dto.Add,
                AdmissionTime=dto.AdmissionTime,
                Birthday=dto.Birthday,
                ContactPerson=dto.ContactPerson,
                DiagnosisName=dto.HosDiagnosisName,
                DischargedTime=dto.DischargedTime,
                ContactPhone=dto.ContactPhone,
                InputCode=dto.InputCode,
                PatientName=dto.PatientName,
                HospitalNo=dto.HospitalNo,
                PatientIdCardNo=dto.PatientIdCardNo,
                HospitalCode=dto.HospitalCode,
                HospitalId=dto.HospitalId,
                DepartmentId=dto.HosDepartmentId,
                MarriageId=dto.HosMarriageId,
                DiagnosisId=dto.HosDiagnosisId,
                WardId=dto.HosWardId,
                WorkId=dto.HosWorkId,
            };
        }
        public void BulkCreat(List<CreateUpdateMedicalRecordsDto> dtos)
        {
            //var records = base.ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(dtos[1]);
            //var records = Map(dtos[1]);

            ConcurrentBag<MedicalRecords> medicalRecords = new ConcurrentBag<MedicalRecords>();

            //var works = await _workRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosWorkId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var departments = await _departmentRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosDepartmentId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var marriages = await _marriageRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosMarriageId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var wards = await _wardRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosWardId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var diagnosis = await _diagnosisRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosDiagnosisName).Distinct().Contains(x.HospitalCode)).ToListAsync();

            //Task.WaitAll(works, departments, marriages, wards, diagnosis);

            Parallel.ForEach(dtos, dto =>
            {
                //medicalRecord.Work = works.FirstOrDefault(x => x.HospitalCode == medicalRecord.WardId);
                //var entity = ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(dto);
                var entity = Map(dto);
                //entity.Work = works.FirstOrDefault(x => x.HospitalCode == dto.HosWorkId);
                //entity.WorkId = entity.Work?.Id;
                //entity.Department = departments.FirstOrDefault(x => x.HospitalCode == dto.HosDepartmentId);
                //entity.DepartmentId = entity.Department?.Id;
                //entity.Marriage = marriages.FirstOrDefault(x => x.HospitalCode == dto.HosMarriageId);
                //entity.MarriageId = entity.Marriage?.Id;
                //entity.Ward = wards.FirstOrDefault(x => x.HospitalCode == dto.HosWardId);
                //entity.WardId = entity.Ward?.Id;
                //entity.Diagnosis = diagnosis.FirstOrDefault(x => x.HospitalCode == dto.HosDiagnosisId);
                //entity.DiagnosisId = entity.Diagnosis?.Id;
                medicalRecords.Add(entity);
            });

            Repository.GetDbContext().BulkInsertAsync(medicalRecords.ToList()).Wait();
            //await _medicalRecordsDapperRepository.BulkCreatAsync(medicalRecords.ToList());
        }

        [RemoteService(IsEnabled = false)]
        public async Task BulkCreatAsync(List<CreateUpdateMedicalRecordsDto> dtos)
        {
            //var records = base.ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(dtos[1]);
            //var records = Map(dtos[1]);

            ConcurrentBag<MedicalRecords> medicalRecords = new ConcurrentBag<MedicalRecords>();

            //var works = await _workRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosWorkId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var departments = await _departmentRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosDepartmentId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var marriages = await _marriageRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosMarriageId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var wards = await _wardRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosWardId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            //var diagnosis = await _diagnosisRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(x => x.HosDiagnosisName).Distinct().Contains(x.HospitalCode)).ToListAsync();

            //Task.WaitAll(works, departments, marriages, wards, diagnosis);

            Parallel.ForEach(dtos, dto =>
            {
                //medicalRecord.Work = works.FirstOrDefault(x => x.HospitalCode == medicalRecord.WardId);
                //var entity = ObjectMapper.Map<CreateUpdateMedicalRecordsDto, MedicalRecords>(dto);
                var entity = Map(dto);
                //entity.Work = works.FirstOrDefault(x => x.HospitalCode == dto.HosWorkId);
                //entity.WorkId = entity.Work?.Id;
                //entity.Department = departments.FirstOrDefault(x => x.HospitalCode == dto.HosDepartmentId);
                //entity.DepartmentId = entity.Department?.Id;
                //entity.Marriage = marriages.FirstOrDefault(x => x.HospitalCode == dto.HosMarriageId);
                //entity.MarriageId = entity.Marriage?.Id;
                //entity.Ward = wards.FirstOrDefault(x => x.HospitalCode == dto.HosWardId);
                //entity.WardId = entity.Ward?.Id;
                //entity.Diagnosis = diagnosis.FirstOrDefault(x => x.HospitalCode == dto.HosDiagnosisId);
                //entity.DiagnosisId = entity.Diagnosis?.Id;
                medicalRecords.Add(entity);
            });

            await Repository.GetDbContext().BulkInsertAsync(medicalRecords.ToList());
            //await _medicalRecordsDapperRepository.BulkCreatAsync(medicalRecords.ToList());
        }
    }
}
