using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 体制数据
    /// </summary>
    [Dapper.Contrib.Extensions.Table("PhysicalSigns")]
    public class PhysicalSigns : HospitalAggregateRoot,IIsDel
    {
        [MaxLength(50)]
        public string MedicalRecordsId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Breathe { set; get; }
        [MaxLength(50)]
        public string HeartRate { set; get; }
        [MaxLength(50)]
        public string Temperature { get; set; }
        public bool IsDel { get; set; }
        [Computed]
        [NotMapped]
        public MedicalRecords MedicalRecords{ get; set; }

    }
}
