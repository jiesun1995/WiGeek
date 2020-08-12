using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Infrastructure.Repositories;

namespace WiGeek.Application
{
    public class OrderService : CrudAppService<Order, OrderDto, int, PagedAndSortedResultRequestDto, CreateUpdateOrderDto, CreateUpdateOrderDto>, IOrderService
    {
        //private readonly IMedicalRecordsService _medicalRecordsService;
        private readonly IRepository<MedicalRecords, int> _medicalRecordsRepository;
        private readonly IRepository<Ward, int> _wardRepository;
        private readonly IRepository<OrderStatus, int> _orderStatusRepository;
        private readonly IRepository<OrderType, int> _orderTypeRepository;
        private readonly IOrderDapperRepository _orderDapperRepository;

        public OrderService(IRepository<Order, int> repository, IRepository<MedicalRecords, int> medicalRecordsRepository, IRepository<Ward, int> wardRepository, IRepository<OrderStatus, int> orderStatusRepository, IRepository<OrderType, int> orderTypeRepository, IOrderDapperRepository orderDapperRepository)
            :base(repository)
        {
            _medicalRecordsRepository = medicalRecordsRepository;
            _wardRepository = wardRepository;
            _orderStatusRepository = orderStatusRepository;
            _orderTypeRepository = orderTypeRepository;
            _orderDapperRepository = orderDapperRepository;
        }

        private Order Map(CreateUpdateOrderDto dto) => new Order
        {
            DepartmentId = dto.DepartmentId,
            Dosage = dto.Dosage,
            Name = dto.Name,
            OpenTime = dto.OpenTime,
            GroupId = dto.GroupId,
            NurseReviewTime = dto.NurseReviewTime,
            NurseExecuteTime=dto.NurseExecuteTime,
            Unit=dto.Unit,
            Remark=dto.Remark,
            HospitalId=dto.HospitalId,
            HospitalCode=dto.HospitalCode,
            Type=dto.Type,            
        };

        public async Task BulkCreatAsync(List<CreateUpdateOrderDto> dtos)
        {
            ConcurrentBag<Order> orders = new ConcurrentBag<Order>();
            var medicalRecords = await _medicalRecordsRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(y => y.HosMedicalRecordsId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            var wards = await _wardRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(y => y.HosWardId).Distinct().Contains(x.HospitalCode)).ToListAsync();
            var orderStatuses = await _orderStatusRepository.Where(x=>dtos.Select(y => y.HosOrderStatusId).Contains(x.HospitalCode)).Distinct().ToListAsync();
            var orderTypes = await _orderTypeRepository.Where(x => x.HospitalId == dtos.First().HospitalId && dtos.Select(y => y.HosOrderTypeId).Distinct().Contains(x.HospitalCode)).ToListAsync();

            Parallel.ForEach(dtos, dto =>
            {
                //medicalRecord.Work = works.FirstOrDefault(x => x.HospitalCode == medicalRecord.WardId);
                //var entity = ObjectMapper.Map<CreateUpdateOrderDto, Order>(dto);
                var entity = Map(dto);
                entity.MedicalRecords = medicalRecords.FirstOrDefault(x => x.HospitalCode == dto.HosMedicalRecordsId);
                entity.Ward = wards.FirstOrDefault(x => x.HospitalCode == dto.HosWardId);
                entity.OrderStatus = orderStatuses.FirstOrDefault(x => x.HospitalCode == dto.HosOrderStatusId);
                entity.OrderType = orderTypes.FirstOrDefault(x => x.HospitalCode == dto.HosOrderTypeId);
                orders.Add(entity);
            });
            await _orderDapperRepository.BulkCreatAsync(orders.ToList());
            //await Repository.GetDbContext().BulkInsertAsync(orders);
        }
    }
}
