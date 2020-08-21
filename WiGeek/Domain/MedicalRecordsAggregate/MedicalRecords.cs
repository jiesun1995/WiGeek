using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Events;
using WiGeek.Domain.DepartmentAggregate;
using WiGeek.Domain.MarriageAggregate;
using WiGeek.Domain.OrderAggregate;
using WiGeek.Domain.SeedWork;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;
using WiGeek.Domain.WorkAggregate;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.MedicalRecordsAggregate
{
    /// <summary>
    /// 就诊记录
    /// </summary>
    [Dapper.Contrib.Extensions.Table("MedicalRecords")]
    public class MedicalRecords : HospitalAggregateRoot
    {

        //protected MedicalRecords() { }
        //public MedicalRecords(string hospitalCode, 
        //    string hospitalId, 
        //    int? workId, 
        //    int? marriageId, 
        //    int? departmentId, 
        //    DateTime? admissionTime,
        //    string hospitalNo, 
        //    string patientName, 
        //    string inputCode,
        //    string sex, 
        //    DateTime? birthday, 
        //    string patientIdCardNo, 
        //    string diagnosisName,
        //    DateTime? dischargedTime, 
        //    int? wardId, 
        //    string contactPerson, 
        //    string contactPhone, 
        //    string add
        //    )
        //{            
        //    HospitalId = hospitalId;
        //    HospitalCode = hospitalCode;
        //    WorkId = workId;
        //    MarriageId = marriageId;
        //    DepartmentId = departmentId;
        //    AdmissionTime = admissionTime;
        //    HospitalNo = hospitalNo;
        //    PatientName = patientName;
        //    InputCode = inputCode;
        //    Sex = sex;
        //    Birthday = birthday;
        //    PatientIdCardNo = patientIdCardNo;
        //    DiagnosisName = diagnosisName;
        //    DischargedTime = dischargedTime;
        //    WardId = wardId;
        //    ContactPerson = contactPerson;
        //    ContactPhone = contactPhone;
        //    Add = add;

        //}

        /// <summary>
        /// 工作
        /// </summary>
        [MaxLength(50)]
        [ColNumber(1)]
        public string WorkId { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        [MaxLength(50)]
        [ColNumber(2)]
        public string MarriageId { set; get; }
        /// <summary>
        /// 科室ID
        /// </summary>
        [MaxLength(50)]
        [ColNumber(3)]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        [ColNumber(4)]
        public DateTime? AdmissionTime { set; get; }
        /// <summary>
        /// 住院号
        /// </summary>
        [MaxLength(50)]
        [ColNumber(5)]
        public string HospitalNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(500)]
        [ColNumber(6)]
        public string PatientName { set; get; }
        /// <summary>
        /// 过滤码
        /// </summary>
        [MaxLength(50)]
        [ColNumber(7)]
        public string InputCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(10)]
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
        [MaxLength(18)]
        [IdCard]
        [ColNumber(10)]
        public string PatientIdCardNo { get; set; }
        /// <summary>
        /// 主要诊断
        /// </summary>
        [MaxLength(500)]
        [ColNumber(11)]
        public string DiagnosisName { get; set; }
        /// <summary>
        /// 诊断Id
        /// </summary>
        [MaxLength(50)]
        public string DiagnosisId { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>        
        [ColNumber(12)]        
        public DateTime? DischargedTime { get; set; }
        /// <summary>
        /// 病区ID
        /// </summary>
        [MaxLength(50)]
        [ColNumber(13)]
        public string WardId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(500)]
        [ColNumber(14)]
        public string ContactPerson { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(100)]
        [ColNumber(15)]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        [MaxLength(500)]
        [ColNumber(16)]
        public string Add { get; set; }
        [Computed]
        [NotMapped]
        public int? Age { get { return DateTime.Now.Year - Birthday?.Year; } }
        [Computed]
        [NotMapped]
        public Department Department { get; set; }
        [Computed]
        [NotMapped]
        public Marriage Marriage { get; set; }
        [Computed]
        [NotMapped]
        public List<Order> Orders { get; set; } = new List<Order>();
        [Computed]
        [NotMapped]
        public Diagnosis Diagnosis { get; set; }
        [Computed]
        [NotMapped]
        public Ward Ward { get; set; }
        [Computed]
        [NotMapped]
        public Work Work { get; set; }
        [Computed]
        [NotMapped]
        public List<PhysicalSigns> PhysicalSigns { get; set; }
    }
}
