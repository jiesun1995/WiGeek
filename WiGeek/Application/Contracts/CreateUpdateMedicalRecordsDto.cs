using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateMedicalRecordsDto: CreateUpdateDomainBaseDto<MedicalRecordsDto>
    {
        /// <summary>
        /// 工作
        /// </summary>
        [ColNumber(1)]
        public string HosWorkId { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        [ColNumber(2)]
        public string HosMarriageId { set; get; }
        /// <summary>
        /// 科室ID
        /// </summary>
        [ColNumber(3)]
        public string HosDepartmentId { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        [ColNumber(4)]
        public DateTime? AdmissionTime { set; get; }
        /// <summary>
        /// 住院号
        /// </summary>
        [ColNumber(5)]
        public string HospitalNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [ColNumber(6)]
        public string PatientName { set; get; }
        /// <summary>
        /// 过滤码
        /// </summary>
        [ColNumber(7)]
        public string InputCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [ColNumber(8)]
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [ColNumber(9)]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [ColNumber(10)]
        [IdCard]
        public string PatientIdCardNo { get; set; }
        /// <summary>
        /// 主要诊断
        /// </summary>
        [ColNumber(11)]
        public string HosDiagnosisName { get; set; }
        /// <summary>
        /// 主要诊断Id
        /// </summary>
        
        public string HosDiagnosisId { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        [ColNumber(12)]
        public DateTime? DischargedTime { get; set; }
        /// <summary>
        /// 病区ID
        /// </summary>
        [ColNumber(13)]
        public string HosWardId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [ColNumber(14)]
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [ColNumber(15)]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        [ColNumber(16)]
        public string Add { get; set; }
        /// <summary>
        /// 体温Id
        /// </summary>
        public List<string> HosPhysicalSignsId { set; get; }
        /// <summary>
        /// 医院医嘱
        /// </summary>
        public List<string> HosOrderIds { get; set; }

    }
}
