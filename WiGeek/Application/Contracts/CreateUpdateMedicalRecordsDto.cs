using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateMedicalRecordsDto: CreateUpdateDomainBaseDto<MedicalRecordsDto>
    {
        /// <summary>
        /// 工作
        /// </summary>
        public string HosWorkId { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        public string HosMarriageId { set; get; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public string HosDepartmentId { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime? AdmissionTime { set; get; }
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
        /// <summary>
        /// 主要诊断
        /// </summary>
        public string HosDiagnosisName { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? DischargedTime { get; set; }
        /// <summary>
        /// 病区ID
        /// </summary>
        public string HosWardId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string Add { get; set; }
        /// <summary>
        /// 体温Id
        /// </summary>
        public string HosPhysicalSignsId { set; get; }
        /// <summary>
        /// 医院医嘱
        /// </summary>
        public List<string> HosOrderIds { get; set; }

    }
}
