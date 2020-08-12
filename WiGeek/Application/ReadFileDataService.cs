﻿using Castle.Core.Internal;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using WiGeek.Application.Contracts;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.SeedWork;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;
using WiGeek.EntityFrameworkCore;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application
{
    public class ReadFileDataService: IReadFileDataService
    {
        private IEnumerable<Ward> _wards ;
        private IEnumerable<Work> _works ;
        private IEnumerable<Marriage> _marriages;
        private IEnumerable<Department> _departments;
        private IEnumerable<OrderType> _orderTypes;
        private IEnumerable<OrderStatus> _orderStatuses;
        //private IEnumerable<Order> _orders;
        private IEnumerable<PhysicalSigns> _physicalSigns;
        private IEnumerable<CreateUpdateMedicalRecordsDto> _medicalRecords;
        private readonly FileInfo[] _fileInfos;
        private readonly IRepository<Ward, int> _wardRepository;
        private readonly IRepository<Work, int> _workRepository;
        private readonly IMedicalRecordsService _medicalRecordsService;
        private readonly IOrderService _orderService;
        private readonly IPhysicalSignsService _physicalSignsService;
        private readonly IRepository<Marriage, int> _marriageRepository;
        private readonly IRepository<Department, int> _departmentRepository;
        private readonly IRepository<OrderType, int> _orderTypeRepository;
        private readonly IRepository<OrderStatus, int> _orderStatusRepository;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<PhysicalSigns, int> _physicalSignsRepository;
        private readonly IRepository<MedicalRecords, int> _medicalRecordsRepository;
        private readonly WiGeekDbContext dbContext;
        private readonly Dictionary<string, string> _hoskey;

        public ReadFileDataService(IPhysicalSignsService physicalSignsService, IOrderService orderService, WiGeekDbContext _dbContext, IConfiguration configuration, IRepository<Ward, int> wardRepository, IRepository<Work, int> workRepository, IRepository<Marriage, int> marriageRepository, IRepository<Department, int> departmentRepository, IRepository<OrderType, int> orderTypeRepository, IRepository<OrderStatus, int> orderStatusRepository, IRepository<Order, int> orderRepository, IRepository<PhysicalSigns, int> physicalSignsRepository, IRepository<MedicalRecords, int> medicalRecordsRepository, IMedicalRecordsService medicalRecordsService)
        {
            //var path= configuration.GetValue<string>("RootPath");
            var path = "E:\\work\\WiGeek\\WiGeek样本数据20200729\\标准比赛数据集";
            var directoryInfo = new DirectoryInfo(path);
            _fileInfos = ReadFileInfos(directoryInfo);
            _wardRepository = wardRepository;
            _workRepository = workRepository;
            _marriageRepository = marriageRepository;
            _departmentRepository = departmentRepository;
            _orderTypeRepository = orderTypeRepository;
            _orderStatusRepository = orderStatusRepository;
            _orderRepository = orderRepository;
            _physicalSignsRepository = physicalSignsRepository;
            _medicalRecordsRepository = medicalRecordsRepository;
            _medicalRecordsService = medicalRecordsService;
            dbContext = _dbContext;
            _orderService = orderService;
            _physicalSignsService = physicalSignsService;
            _hoskey = new Dictionary<string, string>
            {
                {"医院数据1","0" },
                {"医院数据2","1" },
                {"医院数据3","2" },
            };
        }
        private FileInfo[] ReadFileInfos(DirectoryInfo directoryInfo)
        {
            var fileInfos = new List<FileInfo>();
            fileInfos.AddRange(directoryInfo.GetFiles());
            foreach (var directory in directoryInfo.GetDirectories())
            {
                fileInfos.AddRange(ReadFileInfos(directory));
            }
            return fileInfos.ToArray();
        }
        private void ReadFileOneLevel()
        {
            //Parallel.ForEach(_fileInfos, fileInfo =>
            foreach (var fileInfo in _fileInfos)
            {
                if (fileInfo.Name.Contains("病区字典"))
                {
                    _wards = readData<Ward>(fileInfo.FullName);

                    //using (var dbContext = _wardRepository.GetDbContext())
                        dbContext.BulkInsert(_wards.ToList());
                }
                else if (fileInfo.Name.Contains("工作字典"))
                {
                    _works = readData<Work>(fileInfo.FullName);
                    //using (var dbContext = _workRepository.GetDbContext())
                        dbContext.BulkInsert(_works.ToList());
                }
                else if (fileInfo.Name.Contains("婚姻字典"))
                {
                    _marriages = readData<Marriage>(fileInfo.FullName);
                    //using (var dbContext = _marriageRepository.GetDbContext())
                        dbContext.BulkInsert(_marriages.ToList());
                }
                else if (fileInfo.Name.Contains("科室字典"))
                {
                    _departments = readData<Department>(fileInfo.FullName);
                    //using (var dbContext = _departmentRepository.GetDbContext())
                        dbContext.BulkInsert(_departments.ToList());
                }
                else if (fileInfo.Name.Contains("医嘱项目类型字典"))
                {
                    _orderTypes = readData<OrderType>(fileInfo.FullName);
                    //using (var dbContext = _orderTypeRepository.GetDbContext())
                        dbContext.BulkInsert(_orderTypes.ToList());
                }
                else if (fileInfo.Name.Contains("医嘱状态字典"))
                {
                    _orderStatuses = readData<OrderStatus>(fileInfo.FullName);
                    //using (var dbContext = _orderStatusRepository.GetDbContext())
                        dbContext.BulkInsert(_orderStatuses.ToList());
                }
            };
        }
        private void ReadFileTwoLevel()
        {
            //Parallel.ForEach(, async fileInfo =>
            //{
            List<CreateUpdateMedicalRecordsDto> list = new List<CreateUpdateMedicalRecordsDto>();
            foreach (var fileInfo in _fileInfos)
            {
                if (fileInfo.Name.Contains("就诊记录"))
                {
                    _medicalRecords = readData<CreateUpdateMedicalRecordsDto>(fileInfo.FullName);
                    _medicalRecordsService.BulkCreatAsync(_medicalRecords.ToList()).Wait();
                    list.AddRange(_medicalRecords);
                }
            }
            //_medicalRecordsService.BulkCreatAsync(list).Wait();

            //});
        }
        private async Task ReadFileThreeLevel()
        {
            //Parallel.ForEach(, fileInfo =>
            foreach (var fileInfo in _fileInfos)
            {
                //if (fileInfo.Name.Contains("医嘱数据"))
                //{
                //    var _orders = readData<CreateUpdateOrderDto>(fileInfo.FullName);
                //    //_orderRepository.GetDbContext().BulkInsert(_orders.ToList());
                //    await _orderService.BulkCreatAsync(_orders.ToList());
                //}
                //else
                if (fileInfo.Name.Contains("体征数据"))
                {
                    var dtos = readData<CreateUpdatePhysicalSignsDto>(fileInfo.FullName);
                    //_physicalSignsRepository.GetDbContext().BulkInsert(_physicalSigns.ToList());
                    await _physicalSignsService.BulkCreatAsync(dtos.ToList());
                }
            }
        }
        public void ReadFileWriteData()
        {
            //ReadFileOneLevel();
            ReadFileTwoLevel();
            //ReadFileThreeLevel().Wait();
        }

        

        public IEnumerable<T> readData<T>(string FilePath) where T: class,new ()
        {
            var list = new List<T>();
            Dictionary<int, PropertyInfo> PropertyInfoCols = new Dictionary<int, PropertyInfo>();
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var colNumberAttribute = property.GetAttribute<ColNumberAttribute>();
                if (colNumberAttribute != null)
                {
                    PropertyInfoCols.Add(colNumberAttribute.ColNumber, property);
                }
            }
            bool fisrt = true;
            using (TextFieldParser parser = new TextFieldParser(FilePath, Encoding.GetEncoding("GB2312")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    T obj = new T();
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (fisrt)
                            continue;
                        var field = fields[i];
                        if (PropertyInfoCols.ContainsKey(i))
                        {
                            if (PropertyInfoCols[i].PropertyType == typeof(DateTime?))
                                if (DateTime.TryParse(field, out DateTime dt))
                                    PropertyInfoCols[i].SetValue(obj, dt);
                                else
                                    PropertyInfoCols[i].SetValue(obj, null);
                            else
                                PropertyInfoCols[i].SetValue(obj, field);
                        }
                        if(obj is IHospitalId)
                        {
                            var hospitalId = obj as IHospitalId;
                            var key = _hoskey.Keys.FirstOrDefault(x => FilePath.Contains(x));
                            if (_hoskey.Keys.Any(x => FilePath.Contains(x)))
                            {
                                hospitalId.HospitalId = _hoskey[key];
                            }
                        }
                    }
                    if (!fisrt)
                        list.Add(obj);
                    //yield return obj;
                    fisrt = false;
                }
            }
            return list;
        }
    }
}
