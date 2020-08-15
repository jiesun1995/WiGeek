using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Application.Contracts.Report
{
    public class AReportOneDto
    {
        /// <summary>
        /// 医院名称
        /// </summary>
        public string HosName { get; set; }
        /// <summary>
        /// 医院id
        /// </summary>
        public string HosId { get; set; }
        /// <summary>
        /// 科室字典数
        /// </summary>
        public int? Dept { get; set; }
        /// <summary>
        /// 病区字典数
        /// </summary>
        public int? Ward { get; set; }
        /// <summary>
        /// 工作字典数
        /// </summary>
        public int? Work { get; set; }
        /// <summary>
        /// 婚姻字典数
        /// </summary>
        public int? Mar { get; set; }
    }
}
