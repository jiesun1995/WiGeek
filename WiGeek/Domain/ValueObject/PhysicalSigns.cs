using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 体制数据
    /// </summary>
    [Dapper.Contrib.Extensions.Table("PhysicalSigns")]
    public class PhysicalSigns : HospitalAggregateRoot,IIsDel
    {
        public override string HospitalCode { get; set; }
        [MaxLength(50)]
        [ColNumber(0)]
        public string MedicalRecordsId { get; set; }
        [ColNumber(1)]
        public DateTime? CreateTime { get; set; }
        [ColNumber(2)]
        public string Breathe { set; get; }
        [MaxLength(50)]
        [ColNumber(3)]
        public string HeartRate { set; get; }
        [MaxLength(50)]
        [ColNumber(4)]
        public string Temperature { get; set; }
        public bool IsDel { get; set; }
        [Computed]
        [NotMapped]
        public MedicalRecords MedicalRecords{ get; set; }

    }
}
