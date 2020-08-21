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
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.OrderAggregate
{
    /// <summary>
    /// 医嘱
    /// </summary>
    public class Order : HospitalAggregateRoot,IIsDel
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        [ColName("医嘱ID")]
        public override string HospitalCode { get; set; }
        /// <summary>
        /// 就诊唯一ID
        /// </summary>
        [MaxLength(50)]
        [ColName("就诊唯一ID")]
        public string MedicalRecordsId { get; set; }
        /// <summary>
        /// 同组标识
        /// </summary>
        [MaxLength(50)]
        [ColName("同组标识")]
        public string GroupId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        [MaxLength(50)]
        [ColName("科室ID")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [MaxLength(50)]
        [ColName("类型")]
        public string Type { set; get; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        [MaxLength(50)]
        [ColName("医嘱类型")]
        public string OrderTypeId { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(50)]
        [ColName("状态")]
        public string OrderStatusId { get; set; }
        /// <summary>
        /// 开立时间
        /// </summary>
        [ColName("开立时间")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 护士审核时间
        /// </summary>
        [ColName("护士审核时间")]
        public DateTime? NurseReviewTime { set; get; }
        /// <summary>
        /// 护士执行时间
        /// </summary>
        [ColName("护士执行时间")]
        public DateTime? NurseExecuteTime { set; get; }
        /// <summary>
        /// 用量
        /// </summary>
        [MaxLength(50)]
        [ColName("用量")]
        public string Dosage { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [ColName("执行时间")]
        public DateTime? ExecuteTime { get; set; }
        /// <summary>
        /// 用量单位
        /// </summary>
        [MaxLength(50)]
        [ColName("用量单位")]
        public string Unit { set; get; }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        [MaxLength(500)]
        [ColName("医嘱名称")]
        public string Name { set; get; }
        /// <summary>
        /// 医嘱备注
        /// </summary>
        [MaxLength(500)]
        [ColName("医嘱备注")]
        public string Remark { get; set; }
        public bool IsDel { get; set; }
        /// <summary>
        /// 执行病区
        /// </summary>        
        [MaxLength(50)]
        [ColName("执行病区")]
        public string WardId { set; get; }
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
