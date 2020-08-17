using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateOrderDto : CreateUpdateDomainBaseDto<OrderDto>
    {
        [ColName("医嘱ID")]
        public override string HospitalCode { get ; set ; }
        /// <summary>
        /// 就诊唯一ID
        /// </summary>
        [ColName("就诊唯一ID")]
        public string HosMedicalRecordsId { get; set; }
        /// <summary>
        /// 同组标识
        /// </summary>
        [ColName("同组标识")]
        public string GroupId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        [ColName("科室ID")]
        public string HosDepartmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [ColName("类型")]
        public string Type { set; get; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        [ColName("医嘱类型")]
        public string HosOrderTypeId { set; get; }
        /// <summary>
        /// 医嘱状态
        /// </summary>
        [ColName("状态")]
        public string HosOrderStatusId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        
        public string Status { get; set; }
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
        /// <summary>
        /// 用量单位
        /// </summary>
        [ColName("用量单位")]
        public string Unit { set; get; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [ColName("执行时间")]
        public DateTime? ExecuteTime { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [ColName("用量")]
        public string Dosage { get; set; }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        [ColName("医嘱名称")]
        public string Name { set; get; }
        /// <summary>
        /// 医嘱备注
        /// </summary>
        [ColName("医嘱备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 执行病区
        /// </summary>
        [ColName("执行病区")]
        public string HosWardId { set; get; }
    }
}
