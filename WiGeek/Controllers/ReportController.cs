using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using WiGeek.Application;
using WiGeek.Application.Contracts.Report;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;
using WiGeek.Infrastructure.Repositories;

namespace WiGeek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : AbpController
    {
        private readonly IRepository<Ward, int> _wardRepository;
        private readonly IRepository<OrderStatus, int> _orderStatusRepository;
        private readonly IRepository<Department, int> _departmentRepository;
        private readonly IRepository<Work, int> _workRepository;
        private readonly IRepository<Marriage, int> _marriageRepository;

        private readonly IRepository<OrderType, int> _orderTypeRepository;
        private readonly IRepository<MedicalRecords, int> _medicalRecordsRepository;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<Diagnosis, int> _diagnosisRepository;
        private readonly IRepository<PhysicalSigns, int> _physicalSignsRepository;

        private readonly IOrderDapperRepository _orderDapperRepository;
        private readonly IDiagnosesDapperRepository _diagnosesDapperRepository;
        private readonly IPhysicalSignsDapperRepository _physicalSignsDapperRepository;
        //private readonly IUnitOfWork _unitOfWork;

        private List<AReportOneDto> _AReportOneDto;
        private List<AReportTwoDto> _AReportTwoDto;

        public ReportController( IOrderDapperRepository orderDapperRepository, IDiagnosesDapperRepository diagnosesDapperRepository, IPhysicalSignsDapperRepository physicalSignsDapperRepository, IRepository<Ward, int> wardRepository, IRepository<OrderStatus, int> orderStatusRepository, IRepository<Department, int> departmentRepository, IRepository<Work, int> workRepository, IRepository<Marriage, int> marriageRepository, IRepository<OrderType, int> orderTypeRepository, IRepository<MedicalRecords, int> medicalRecordsRepository, IRepository<Order, int> orderRepository, IRepository<Diagnosis, int> diagnosisRepository, IRepository<PhysicalSigns, int> physicalSignsRepository)
        {
            _wardRepository = wardRepository;
            _orderStatusRepository = orderStatusRepository;
            _departmentRepository = departmentRepository;
            _workRepository = workRepository;
            _marriageRepository = marriageRepository;
            _orderTypeRepository = orderTypeRepository;
            _medicalRecordsRepository = medicalRecordsRepository;
            _orderRepository = orderRepository;
            _diagnosisRepository = diagnosisRepository;
            _physicalSignsRepository = physicalSignsRepository;
            _orderDapperRepository = orderDapperRepository;
            _diagnosesDapperRepository = diagnosesDapperRepository;
            _physicalSignsDapperRepository = physicalSignsDapperRepository;


            _AReportOneDto = new List<AReportOneDto>
            {
                new AReportOneDto{ HosName="医院1",HosId="0" },
                new AReportOneDto{ HosName="医院2" ,HosId="1"},
                new AReportOneDto{ HosName="医院3" ,HosId="2"}
            };
            _AReportTwoDto = new List<AReportTwoDto>
            {
                new AReportTwoDto{ HosName="医院1",HosId="0" },
                new AReportTwoDto{ HosName="医院2" ,HosId="1"},
                new AReportTwoDto{ HosName="医院3" ,HosId="2"}
            };
        }

        [HttpGet("AReportOne")]
        public IEnumerable<AReportOneDto> One()
        {
            var departmentCount = _departmentRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var wardCount = _wardRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var workCount = _workRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var marriageCount = _marriageRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            _AReportOneDto.ForEach(x =>
            {
                x.Dept = departmentCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.Ward = wardCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.Mar = marriageCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.Work = workCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
            });
            return _AReportOneDto;
        }
        [HttpGet("AReportTwo")]
        public IEnumerable<AReportTwoDto> Two()
        {
            var orderTypeCount = _orderTypeRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var medicalRecordsCount = _medicalRecordsRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var orderCount = _orderRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var diagnosisCount = _diagnosisRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            var physicalSignsCount = _physicalSignsRepository.GroupBy(x => x.HospitalId).Select(x => new { HosId = x.Key, count = x.Count() });
            _AReportTwoDto.ForEach(x =>
            {
                x.MedicalRecords = medicalRecordsCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.Order = orderCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.OrderType = orderTypeCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.PhysicalSigns = physicalSignsCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
                x.Diagnosis = diagnosisCount.FirstOrDefault(y => y.HosId == x.HosId)?.count;
            });
            return _AReportTwoDto;
        }

        
        [HttpGet("clear")]
        public async Task<IActionResult> ClearData()
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
               await _diagnosesDapperRepository.DelNotInMedicalRecords();
               await _orderDapperRepository.DelNotInMedicalRecords();
               await _physicalSignsDapperRepository.DelNotInMedicalRecords();
               await unitOfWork.SaveChangesAsync();
            }
            return Ok(new
            {
                ///原医嘱数据数
                OldOrderCount = _orderRepository.Count(),
                ///原诊断数据数
                OldDiagnosesCount = _diagnosisRepository.Count(),
                ///原体征数据数
                OldPhysicalSigns = _physicalSignsRepository.Count(),
                ///现医嘱数据数
                NowOrderCount = _orderRepository.Where(x=>x.IsDel==false).Count(),
                ///现诊断数据数
                NowDiagnosesCount = _diagnosisRepository.Where(x => x.IsDel == false).Count(),
                ///现体征数据数
                NowPhysicalSigns = _physicalSignsRepository.Where(x => x.IsDel == false).Count(),
            });
        }
    }
}
