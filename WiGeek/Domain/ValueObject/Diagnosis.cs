using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 诊断记录
    /// </summary
    public class Diagnosis : HospitalAggregateRoot
    {
        /// <summary>
        /// 诊断名称
        /// </summary>
        [MaxLength(500)]
        public string Name { get; set; }
        /// <summary>
        /// 诊断类型
        /// </summary>
        [MaxLength(50)]
        public string DiagnosisType { get; set; }
    }
}
