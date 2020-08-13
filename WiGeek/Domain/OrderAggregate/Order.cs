using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.MedicalRecordsAggregate;
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
        [MaxLength(50)]
        public int? MedicalRecordsId { get; set; }
        /// <summary>
        /// 同组标识
        /// </summary>
        [MaxLength(50)]
        public string GroupId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [MaxLength(50)]
        public string Type { set; get; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        public int? OrderTypeId { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? OrderStatusId { get; set; }
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
        [MaxLength(50)]
        public string Dosage { get; set; }
        /// <summary>
        /// 用量单位
        /// </summary>
        [MaxLength(500)]
        public string Unit { set; get; }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        [MaxLength(500)]
        public string Name { set; get; }
        /// <summary>
        /// 医嘱备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 执行病区
        /// </summary>        
        public int? WardId { set; get; }
        [NotMapped]
        public Ward Ward { get; set; }
        [NotMapped]
        public OrderType OrderType { set; get; }
        [NotMapped]
        public MedicalRecords MedicalRecords { get; set; }
        [NotMapped]
        public OrderStatus OrderStatus { set; get; }
    }
    
}
