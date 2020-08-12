using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 体制数据
    /// </summary>
    [Table("PhysicalSigns")]
    public class PhysicalSigns : HospitalAggregateRoot
    {
        public string MedicalRecordsId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Breathe { set; get; }
        public string HeartRate { set; get; }
        public string Temperature { get; set; }
        [Computed]
        public MedicalRecords MedicalRecords{ get; set; }
    }
}
