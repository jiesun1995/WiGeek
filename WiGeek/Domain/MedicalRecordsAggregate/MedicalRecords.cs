using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
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

namespace WiGeek.Domain.MedicalRecordsAggregate
{
    /// <summary>
    /// 就诊记录
    /// </summary>
    [Table("MedicalRecords")]
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
        public int? WorkId { get; set; }
        /// <summary>
        /// 婚姻
        /// </summary>
        public int? MarriageId { set; get; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public int? DepartmentId { get; set; }
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
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PatientIdCardNo { get; set; }
        /// <summary>
        /// 主要诊断
        /// </summary>
        public string DiagnosisName { get; set; }
        /// <summary>
        /// 诊断Id
        /// </summary>
        public int? DiagnosisId { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? DischargedTime { get; set; }
        /// <summary>
        /// 病区ID
        /// </summary>
        public int? WardId { get; set; }
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
        [Computed]
        public int? Age { get { return DateTime.Now.Year - Birthday?.Year; } }
        [Computed]
        public Department Department { get; set; }
        [Computed]
        public Marriage Marriage { get; set; }
        [Computed]
        public List<Order> Orders { get; set; } = new List<Order>();
        [Computed]
        public Diagnosis Diagnosis { get; set; }
        [Computed]
        public Ward Ward { get; set; }
        [Computed]
        public Work Work { get; set; }
        [Computed]
        public List<PhysicalSigns> PhysicalSigns { get; set; }
    }
}
