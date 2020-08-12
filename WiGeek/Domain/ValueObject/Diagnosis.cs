using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        /// <summary>
        /// 诊断类型
        /// </summary>
        public string DiagnosisType { get; set; }
    }
}
