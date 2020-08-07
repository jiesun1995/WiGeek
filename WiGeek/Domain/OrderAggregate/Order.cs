using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Domain.ValueObject;
using WiGeek.Domain.WardAggregate;

namespace WiGeek.Domain.OrderAggregate
{
    /// <summary>
    /// 医嘱
    /// </summary>
    public class Order : HospitalAggregateRoot
    {
        /// <summary>
        /// 就诊唯一ID
        /// </summary>
        public string MedicalRecordsId { get; set; }
        /// <summary>
        /// 同组标识
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { set; get; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        public int? OrderTypeId { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 开立时间
        /// </summary>
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 护士审核时间
        /// </summary>
        public DateTime? NurseReviewTime { set; get; }
        /// <summary>
        /// 护士执行时间
        /// </summary>
        public DateTime? NurseExecuteTime { set; get; }
        /// <summary>
        /// 用量
        /// </summary>
        public string Dosage { get; set; }
        /// <summary>
        /// 用量单位
        /// </summary>
        public string Unit { set; get; }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 医嘱备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 执行病区
        /// </summary>
        public int? WardId { set; get; }
        public Ward Ward { get; set; }
        public OrderType OrderType { set; get; }
    }
    
}
