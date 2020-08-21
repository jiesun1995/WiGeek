using Castle.Core.Internal;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using NUglify.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
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
        private readonly FileInfo[] _fileInfos;
        private readonly IMedicalRecordsService _medicalRecordsService;
        private readonly IOrderService _orderService;
        private readonly IPhysicalSignsService _physicalSignsService;
        private readonly WiGeekDbContext _dbContext;
        private readonly Dictionary<string, string> _hoskey;
        private readonly ILogger<ReadFileDataService> _logger;
        private readonly string _connectionString;

        public ReadFileDataService(IConfiguration configuration, IHostEnvironment hostEnvironment, IMedicalRecordsService medicalRecordsService, IOrderService orderService, IPhysicalSignsService physicalSignsService, WiGeekDbContext dbContext,  ILogger<ReadFileDataService> logger)
        {
            string path;
            _medicalRecordsService = medicalRecordsService;
            _orderService = orderService;
            _physicalSignsService = physicalSignsService;
            _dbContext = dbContext;
            _logger = logger;

            if (hostEnvironment.IsDevelopment())
                //path = "C:\\data\\WiGeek样本数据20200729\\标准比赛数据集";
                path = "E:\\work\\WiGeek\\WiGeek样本数据20200729\\标准比赛数据集";
            else
                path = "D:\\wndata\\[公共素材]\\2020年第一届科技节样本素材\\WiGeek样本数据";
            var directoryInfo = new DirectoryInfo(path);
            _fileInfos = ReadFileInfos(directoryInfo);
            _connectionString = configuration.GetConnectionString("Default");
            _hoskey = new Dictionary<string, string>
            {
                {"医院数据1","0" },
                {"医院数据2","1" },
                {"医院数据3","2" },
            };
        }
        private static string ChangeToDecimal(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("E"))
            {
                if (!decimal.TryParse(strData,System.Globalization.NumberStyles.Float,null, out dData))
                    return strData;
            }
            else
            {
                if (!decimal.TryParse(strData, out dData))
                    return strData;
            }
            return Math.Round(dData, 4).ToString(CultureInfo.InvariantCulture);
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
            var wards = new ConcurrentBag<Ward>();
            var works = new ConcurrentBag<Work>();
            var marriages = new ConcurrentBag<Marriage>();
            var departments = new ConcurrentBag<Department>();
            var orderTypes = new ConcurrentBag<OrderType>();
            var orderStatuses = new ConcurrentBag<OrderStatus>();

            Parallel.ForEach(_fileInfos, fileInfo =>
            //foreach (var fileInfo in _fileInfos)
            {
                if (fileInfo.Name.Contains("病区字典"))
                {
                    readData<Ward>(ref wards, fileInfo.FullName);
                    //dbContext.BulkInsert(_wards.ToList());
                }
                else if (fileInfo.Name.Contains("工作字典"))
                {
                    readData<Work>(ref works, fileInfo.FullName);
                    //using (var dbContext = _workRepository.GetDbContext())
                    //dbContext.BulkInsert(_works.ToList());
                }
                else if (fileInfo.Name.Contains("婚姻字典"))
                {
                    readData<Marriage>(ref marriages, fileInfo.FullName);
                    //using (var dbContext = _marriageRepository.GetDbContext())
                    //dbContext.BulkInsert(_marriages.ToList());
                }
                else if (fileInfo.Name.Contains("科室字典"))
                {
                    readData<Department>(ref departments, fileInfo.FullName);
                    //using (var dbContext = _departmentRepository.GetDbContext())
                    //dbContext.BulkInsert(_departments.ToList());
                }
                else if (fileInfo.Name.Contains("医嘱项目类型字典"))
                {
                    readData<OrderType>(ref orderTypes, fileInfo.FullName);
                    //using (var dbContext = _orderTypeRepository.GetDbContext())
                    //dbContext.BulkInsert(_orderTypes.ToList());
                }
                else if (fileInfo.Name.Contains("医嘱状态字典"))
                {
                    readData<OrderStatus>(ref orderStatuses, fileInfo.FullName);
                    //using (var dbContext = _orderStatusRepository.GetDbContext())
                    //dbContext.BulkInsert(_orderStatuses.ToList());
                }


            });
            _dbContext.BulkInsert(wards.ToList());
            _dbContext.BulkInsert(works.ToList());
            _dbContext.BulkInsert(marriages.ToList());
            _dbContext.BulkInsert(departments.ToList());
            _dbContext.BulkInsert(orderTypes.ToList());
            _dbContext.BulkInsert(orderStatuses.ToList());

        }
        private void ReadFileTwoLevel()
        {
            ConcurrentBag<CreateUpdateMedicalRecordsDto> list = new ConcurrentBag<CreateUpdateMedicalRecordsDto>();

            Parallel.ForEach(_fileInfos, fileInfo =>
            {
            //foreach (var fileInfo in _fileInfos)
            //{
                if (fileInfo.Name.Contains("就诊记录"))
                {
                    readData<CreateUpdateMedicalRecordsDto>(ref list, fileInfo.FullName);
                    //_medicalRecordsService.BulkCreatAsync(_medicalRecords.ToList()).Wait();
                    //list.Add(_medicalRecords);
                }
            //}
            });
            _medicalRecordsService.BulkCreat(list.ToList());
        }
        private async Task BulkCreatAsync<T>(ConcurrentBag<T> list,Func<IEnumerable<T>,Task> creat)
        {
            var count = 1000000;
            for (int i = 0; i < list.Count; i=i+ count)
            {
                var data = list.Skip(i).Take(count);
                await creat(data);
            }
        }
        private async Task ReadFileThreeLevel()
        {
            var list = new ConcurrentBag<CreateUpdatePhysicalSignsDto>();
            var createUpdateOrders = new ConcurrentBag<CreateUpdateOrderDto>();
            Parallel.ForEach(_fileInfos, fileInfo =>
            //foreach (var fileInfo in _fileInfos)
            {

                //var physicalSignsService = _serviceProvider.GetRequiredService<IPhysicalSignsService>();
                if (fileInfo.Name.Contains("医嘱数据"))
                {
                    //await readAndWrite<CreateUpdateOrderDto>(fileInfo.FullName, async data =>
                    // {
                    //     await _orderService.BulkCreatAsync(data.ToList());
                    // });
                    readData<CreateUpdateOrderDto>(ref createUpdateOrders, fileInfo.FullName);
                    //await _orderService.BulkCreatAsync(createUpdateOrders.ToList());
                }
                //else
                //if (fileInfo.Name.Contains("体征数据"))
                //{
                //    //await readAndWrite<CreateUpdatePhysicalSignsDto>(fileInfo.FullName, async data =>
                //    // {
                //    //     await _physicalSignsService.BulkCreatAsync(data.ToList());
                //    // },500000);
                //    readData<CreateUpdatePhysicalSignsDto>(ref list, fileInfo.FullName);
                //    //await _physicalSignsService.BulkCreatAsync(list.ToList());
                //}
            //}
            });

            await BulkCreatAsync(createUpdateOrders, async data =>
             {
                 await _orderService.BulkCreatAsync(data.ToList());
             });
            await BulkCreatAsync(list, async data =>
             {
                 await _physicalSignsService.BulkCreatAsync(data.ToList());
             });
            list.Clear();
            createUpdateOrders.Clear();
            //await _orderService.BulkCreatAsync(createUpdateOrders.ToList());
            //await _physicalSignsService.BulkCreatAsync(list.ToList());
        }
        private void ReadFile()
        {
            //var wards = new ConcurrentBag<Ward>();
            //var works = new ConcurrentBag<Work>();
            //var marriages = new ConcurrentBag<Marriage>();
            //var diagnoses = new ConcurrentBag<Diagnosis>();
            //var departments = new ConcurrentBag<Department>();
            //var orderTypes = new ConcurrentBag<OrderType>();
            //var orderStatuses = new ConcurrentBag<OrderStatus>();
            //var createUpdateMedicalRecords = new ConcurrentBag<CreateUpdateMedicalRecordsDto>();
            //var createUpdatePhysicalSigns = new ConcurrentBag<CreateUpdatePhysicalSignsDto>();
            //var createUpdateOrders = new ConcurrentBag<CreateUpdateOrderDto>();

            //Parallel.ForEach(_fileInfos, fileInfo =>
            Parallel.ForEach(_fileInfos, fileInfo =>
            {
                //var fileInfo = obj as FileInfo;
                if (fileInfo.FullName.Contains("病区字典"))
                {
                    readAndWrite<Ward>(fileInfo.FullName, "Wards", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("工作字典"))
                {
                    readAndWrite<Work>(fileInfo.FullName, "Works", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("诊断数据"))
                {
                    readAndWrite<Diagnosis>(fileInfo.FullName, "Diagnoses", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("婚姻字典"))
                {
                    readAndWrite<Marriage>(fileInfo.FullName, "Marriages", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("科室字典"))
                {
                    readAndWrite<Department>(fileInfo.FullName, "Departments", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("医嘱项目类型字典"))
                {
                    readAndWrite<OrderType>(fileInfo.FullName, "OrderTypes", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else if (fileInfo.FullName.Contains("医嘱状态字典"))
                {
                    readAndWrite<OrderStatus>(fileInfo.FullName, "OrderStatuses", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else
                if (fileInfo.FullName.Contains("就诊记录"))
                {
                    readAndWrite<MedicalRecords>(fileInfo.FullName, "MedicalRecords", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else
                if (fileInfo.FullName.Contains("医嘱数据"))
                {
                    readAndWrite<Order>(fileInfo.FullName, "Orders", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
                else
                if (fileInfo.FullName.Contains("体征数据"))
                {
                    readAndWrite<PhysicalSigns>(fileInfo.FullName, "PhysicalSigns", (dt, maps) =>
                    {
                        return BulkCreat(dt, maps);
                    }).Wait();
                }
            });
            //_fileInfos.ForEach(fileInfo =>
            //{
            //    if (fileInfo.FullName.Contains("病区字典"))
            //    {
            //        readAndWrite<Ward>(fileInfo.FullName, "Wards", (dt, maps) =>
            //         {
            //             return BulkCreat(dt, maps);
            //         }).Wait();
            //        //readData<Ward>(ref wards, fileInfo.FullName);
            //        //dbContext.BulkInsert(_wards.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("工作字典"))
            //    {
            //        readAndWrite<Work>(fileInfo.FullName, "Works", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<Work>(ref works, fileInfo.FullName);
            //        //using (var dbContext = _workRepository.GetDbContext())
            //        //dbContext.BulkInsert(_works.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("诊断数据"))
            //    {
            //        readAndWrite<Diagnosis>(fileInfo.FullName, "Diagnoses", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<Diagnosis>(ref diagnoses, fileInfo.FullName);
            //        //using (var dbContext = _workRepository.GetDbContext())
            //        //dbContext.BulkInsert(_works.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("婚姻字典"))
            //    {
            //        readAndWrite<Marriage>(fileInfo.FullName, "Marriages", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<Marriage>(ref marriages, fileInfo.FullName);
            //        //using (var dbContext = _marriageRepository.GetDbContext())
            //        //dbContext.BulkInsert(_marriages.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("科室字典"))
            //    {
            //        readAndWrite<Department>(fileInfo.FullName, "Departments", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<Department>(ref departments, fileInfo.FullName);
            //        //using (var dbContext = _departmentRepository.GetDbContext())
            //        //dbContext.BulkInsert(_departments.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("医嘱项目类型字典"))
            //    {
            //        readAndWrite<OrderType>(fileInfo.FullName, "OrderTypes", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<OrderType>(ref orderTypes, fileInfo.FullName);
            //        //using (var dbContext = _orderTypeRepository.GetDbContext())
            //        //dbContext.BulkInsert(_orderTypes.ToList());
            //    }
            //    else if (fileInfo.FullName.Contains("医嘱状态字典"))
            //    {
            //        readAndWrite<OrderStatus>(fileInfo.FullName, "OrderStatuses", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<OrderStatus>(ref orderStatuses, fileInfo.FullName);
            //        //using (var dbContext = _orderStatusRepository.GetDbContext())
            //        //dbContext.BulkInsert(_orderStatuses.ToList());
            //    }
            //    else
            //    if (fileInfo.FullName.Contains("就诊记录"))
            //    {
            //        readAndWrite<MedicalRecords>(fileInfo.FullName, "MedicalRecords", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //readData<CreateUpdateMedicalRecordsDto>(ref createUpdateMedicalRecords, fileInfo.FullName);
            //        //_medicalRecordsService.BulkCreatAsync(_medicalRecords.ToList()).Wait();
            //        //list.Add(_medicalRecords);
            //    }
            //    else
            //    if (fileInfo.FullName.Contains("医嘱数据"))
            //    {
            //        readAndWrite<Order>(fileInfo.FullName, "Orders", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //await readAndWrite<CreateUpdateOrderDto>(fileInfo.FullName, async data =>
            //        // {
            //        //     await _orderService.BulkCreatAsync(data.ToList());
            //        // });
            //        //readData<CreateUpdateOrderDto>(ref createUpdateOrders, fileInfo.FullName);
            //        //await _orderService.BulkCreatAsync(createUpdateOrders.ToList());
            //    }
            //    else
            //    if (fileInfo.FullName.Contains("体征数据"))
            //    {
            //        readAndWrite<PhysicalSigns>(fileInfo.FullName, "PhysicalSigns", (dt, maps) =>
            //        {
            //            return BulkCreat(dt, maps);
            //        }).Wait();
            //        //await readAndWrite<CreateUpdatePhysicalSignsDto>(fileInfo.FullName, async data =>
            //        // {
            //        //     await _physicalSignsService.BulkCreatAsync(data.ToList());
            //        // },500000);
            //        //readData<CreateUpdatePhysicalSignsDto>(ref createUpdatePhysicalSigns, fileInfo.FullName);
            //        //await _physicalSignsService.BulkCreatAsync(list.ToList());
            //    }
            //});
            //_dbContext.BulkInsert(wards.ToList());
            //_logger.LogInformation($"完成病区字典数据导入，总共{wards.Count}条");
            //wards.Clear();
            //_dbContext.BulkInsert(diagnoses.ToList());
            //_logger.LogInformation($"完成诊断字典数据导入,总共{diagnoses.Count}条");
            //diagnoses.Clear();
            //_dbContext.BulkInsert(works.ToList());
            //_logger.LogInformation($"完成工作字典数据导入,总共{works.Count}条");
            //works.Clear();
            //_dbContext.BulkInsert(marriages.ToList());
            //_logger.LogInformation($"完成婚姻字典数据导入,总共{marriages.Count}条");
            //marriages.Clear();
            //_dbContext.BulkInsert(departments.ToList());
            //_logger.LogInformation($"完成科室字典数据导入,总共{departments.Count}条");
            //departments.Clear();
            //_dbContext.BulkInsert(orderTypes.ToList());
            //_logger.LogInformation($"完成医嘱类型字典数据导入,总共{orderTypes.Count}条");
            //orderTypes.Clear();
            //_dbContext.BulkInsert(orderStatuses.ToList());
            //_logger.LogInformation($"完成医嘱状态字典数据导入,总共{orderStatuses.Count}条");
            //orderStatuses.Clear();
            //_medicalRecordsService.BulkCreat(createUpdateMedicalRecords.ToList());
            //_logger.LogInformation($"完成就诊记录数据导入,总共{createUpdateMedicalRecords.Count}条");
            //createUpdateMedicalRecords.Clear();
            //await BulkCreatAsync(createUpdateOrders, async data =>
            //{
            //    await _orderService.BulkCreatAsync(data.ToList());
            //});
            //_logger.LogInformation($"完成医嘱数据导入,总共{createUpdateOrders.Count}条");
            //createUpdateOrders.Clear();
            //await BulkCreatAsync(createUpdatePhysicalSigns, async data =>
            //{
            //    await _physicalSignsService.BulkCreatAsync(data.ToList());
            //});
            //_logger.LogInformation($"完成体征数据导入,总共{createUpdatePhysicalSigns.Count}条");
            //createUpdatePhysicalSigns.Clear();
            //GC.Collect();
        }
        public void ReadFileWriteData()
        {
            _logger.LogInformation("开始数据导入");
            ReadFile();
            _logger.LogInformation("数据导入完成");
        }
        public void ReadFileWriteDataByLevel()
        {
            _logger.LogInformation("开始数据导入");
            ReadFileOneLevel();
            ReadFileTwoLevel();
            ReadFileThreeLevel().Wait();
            _logger.LogInformation("数据导入完成");
        }

        public void readData<T>(ref ConcurrentBag<T>  list, string FilePath) where T: class,new ()
        {
            var PropertyInfoCols = new Dictionary<int, PropertyInfo>();
            var PropertyInfoColNames = new Dictionary<string, PropertyInfo>();
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var colNumberAttribute = property.GetAttribute<ColNumberAttribute>();
                if (colNumberAttribute != null)
                    PropertyInfoCols.Add(colNumberAttribute.ColNumber, property);
                var colNameAttribute = property.GetAttribute<ColNameAttribute>();
                if (colNameAttribute != null)
                    PropertyInfoColNames.Add(colNameAttribute.ColName, property);
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
                        var field = fields[i];
                        if (fisrt)
                        {
                            if (PropertyInfoColNames.ContainsKey(field.Trim()))
                            {
                                PropertyInfoCols.Add(i, PropertyInfoColNames[field]);
                            }
                            continue;
                        }
                        if (PropertyInfoCols.ContainsKey(i))
                        {
                            try
                            {
                                if (PropertyInfoCols[i].PropertyType == typeof(DateTime?))
                                    if (DateTime.TryParse(field, out DateTime dt))
                                        PropertyInfoCols[i].SetValue(obj, dt);
                                    else
                                        PropertyInfoCols[i].SetValue(obj, null);
                                else if (PropertyInfoCols[i].PropertyType == typeof(int?))
                                    if (string.IsNullOrEmpty(field))
                                        PropertyInfoCols[i].SetValue(obj, null);
                                    else
                                        PropertyInfoCols[i].SetValue(obj, int.Parse(field));
                                else if (PropertyInfoCols[i].GetAttribute<IdCardAttribute>() != null)
                                    PropertyInfoCols[i].SetValue(obj, ChangeToDecimal(field));
                                else
                                    PropertyInfoCols[i].SetValue(obj, field);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"{FilePath}:{PropertyInfoCols[i].Name} {field} :{ex.StackTrace}");
                                throw ex;
                            }
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
        }

        public async Task readAndWrite<T>(string FilePath,Func<IList<T>,Task> write, int length=100000) where T:class,new()
        {
            List<T> list = new List<T>(length);
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
                        if (obj is IHospitalId)
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
                    {
                        list.Add(obj);
                        if (list.Count >= length)
                        {
                            await write(list);
                            list.Clear();
                        }
                    }
                    //yield return obj;
                    fisrt = false;
                }
                if (list.Count > 0)
                {
                    await write(list);
                    list.Clear();
                }
            }            
        }

        private DataTable CreatDataTable<T>(string tableName,out List<SqlBulkCopyColumnMapping> maps,out Dictionary<int, PropertyInfo> propertyInfoCols, out Dictionary<string, PropertyInfo> PropertyInfoColNames) where T : class, new()
        {
            DataTable dataTable = new DataTable(tableName);
            maps = new List<SqlBulkCopyColumnMapping>();
            propertyInfoCols = new Dictionary<int, PropertyInfo>();
            PropertyInfoColNames = new Dictionary<string, PropertyInfo>();
            Type type = typeof(T);

            var obj = new T();

            if (obj is IHospitalId)
            {
                dataTable.Columns.Add(new DataColumn("HospitalId", typeof(int)));
                maps.Add(new SqlBulkCopyColumnMapping("HospitalId", "HospitalId"));
            }
            if (obj is IIsDel)
            {
                dataTable.Columns.Add(new DataColumn("IsDel", typeof(int)));
                maps.Add(new SqlBulkCopyColumnMapping("IsDel", "IsDel"));
            }
            foreach (var property in type.GetProperties())
            {
                var colNumberAttribute = property.GetAttribute<ColNumberAttribute>();
                if (colNumberAttribute != null)
                {
                    propertyInfoCols.Add(colNumberAttribute.ColNumber, property);
                    if (property.PropertyType == typeof(DateTime?))
                        dataTable.Columns.Add(new DataColumn(property.Name, typeof(DateTime)));
                    else if (property.PropertyType == typeof(int?))
                    {
                        dataTable.Columns.Add(new DataColumn(property.Name, typeof(int)));
                    }
                    else
                        dataTable.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                    maps.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                }
                var colNameAttribute = property.GetAttribute<ColNameAttribute>();
                if (colNameAttribute != null)
                {
                    PropertyInfoColNames.Add(colNameAttribute.ColName, property);
                    if (property.PropertyType == typeof(DateTime?))
                        dataTable.Columns.Add(new DataColumn(property.Name, typeof(DateTime)));
                    else if (property.PropertyType == typeof(int?))
                    {
                        dataTable.Columns.Add(new DataColumn(property.Name, typeof(int)));
                    }
                    else
                        dataTable.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                    maps.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                }
            }
            return dataTable;
        }

        public async Task readAndWrite<T>(string FilePath,string tableName, Func<DataTable,List<SqlBulkCopyColumnMapping>,Task> write, int length = 200000) where T : class, new()
        {
            Task task=Task.CompletedTask;
            _logger.LogInformation($"{FilePath}:开始导入");
            var dataTable = CreatDataTable<T>(tableName, out List<SqlBulkCopyColumnMapping> maps, out Dictionary<int, PropertyInfo> propertyInfoCols, out Dictionary<string, PropertyInfo> PropertyInfoColNames);
            bool fisrt = true;
            using (TextFieldParser parser = new TextFieldParser(FilePath, Encoding.GetEncoding("GB2312")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var dr= dataTable.NewRow();
                    string[] fields = parser.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        var field = fields[i];
                        if (fisrt)
                        {
                             if (PropertyInfoColNames.ContainsKey(field.Trim()))
                            {
                                propertyInfoCols.Add(i, PropertyInfoColNames[field.Trim()]);
                            }
                            continue;
                        }
                        if (propertyInfoCols.ContainsKey(i))
                        {
                            DataColumn col = dataTable.Columns[propertyInfoCols[i].Name];
                            if (col.DataType == typeof(DateTime))
                            {
                                if (DateTime.TryParse(field, out DateTime dt))
                                    dr[propertyInfoCols[i].Name] = dt;
                            }
                            else if (col.DataType == typeof(int))
                            {
                                if (int.TryParse(field, out int number))
                                    dr[propertyInfoCols[i].Name] = number;
                            }
                            else if (propertyInfoCols[i].GetAttribute<IdCardAttribute>() != null)
                            {
                                dr[propertyInfoCols[i].Name] = ChangeToDecimal(field);
                            }
                            else
                                dr[propertyInfoCols[i].Name] = field;
                        }
                        if (dataTable.Columns.Contains("HospitalId"))
                        {
                            var key = _hoskey.Keys.FirstOrDefault(x => FilePath.Contains(x));
                            if (_hoskey.Keys.Any(x => FilePath.Contains(x)))
                            {
                                dr["HospitalId"] = _hoskey[key];
                            }
                        }
                        if (dataTable.Columns.Contains("IsDel"))
                        {
                            dr["IsDel"] = 0;
                        }
                    }
                    if (!fisrt)
                    {
                        dataTable.Rows.Add(dr);
                        if (dataTable.Rows.Count >= length)
                        {
                            await task;
                            task = write(dataTable, maps);
                            dataTable = dataTable.Clone();
                            //dataTable = CreatDataTable<T>(tableName, out maps, out propertyInfoCols, out PropertyInfoColNames);
                        }
                    }
                    fisrt = false;
                }
                if (dataTable.Rows.Count > 0)
                {
                    await task;
                    task = write(dataTable, maps);
                    await task;                    
                }
            }
            _logger.LogInformation($"{FilePath}:导入完成");
            //dataTable.Dispose();
        }

        public Task BulkCreat(DataTable dt,List<SqlBulkCopyColumnMapping> columnMappings)
        {
            return Task.Factory.StartNew(() =>
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(con)
                    {
                        DestinationTableName = dt.TableName,
                        BatchSize = dt.Rows.Count,
                        BulkCopyTimeout = 0,
                    };
                    columnMappings.ForEach(x => bulkCopy.ColumnMappings.Add(x));
                    con.Open();
                    bulkCopy.WriteToServer(dt);
                }
            });
        }

    }
}
