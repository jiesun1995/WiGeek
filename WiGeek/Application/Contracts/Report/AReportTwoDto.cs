using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Application.Contracts.Report
{
    public class AReportTwoDto
    {
        /// <summary>
        ///医院名称
        /// </summary>
        public string HosName { get; set; }
        /// <summary>
        /// 医院ID
        /// </summary>
        public string HosId { get; set; }
        /// <summary>
        /// 医嘱类型数
        /// </summary>
        public int? OrderType { get; set; }
        /// <summary>
        /// 就诊记录数
        /// </summary>
        public int? MedicalRecords { get; set; }
        /// <summary>
        /// 医嘱数
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// 诊断记录数
        /// </summary>
        public int? Diagnosis { get; set; }
        /// <summary>
        /// 体征记录数
        /// </summary>
        public int? PhysicalSigns { get; set; }
    }
}
