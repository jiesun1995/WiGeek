using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Application.Contracts.Report
{
    public class DReportDto
    {
        /// <summary>
        /// 就诊唯一ID
        /// </summary>
        public string MedicalRecordsId { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { set; get; }
        /// <summary>
        /// 过滤码
        /// </summary>
        public string InputCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PatientIdCardNo { get; set; }
    }

    public class PatientDto
    {
        
    }
}
