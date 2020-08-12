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
        /// <summary>
        /// 就诊唯一ID
        /// </summary>
        [ColNumber(1)]
        public string HosMedicalRecordsId { get; set; }
        /// <summary>
        /// 同组标识
        /// </summary>
        [ColNumber(2)]
        public string GroupId { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>
        [ColNumber(3)]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [ColNumber(4)]
        public string Type { set; get; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        [ColNumber(5)]
        public string HosOrderTypeId { set; get; }
        /// <summary>
        /// 医嘱状态
        /// </summary>
        [ColNumber(6)]
        public string HosOrderStatusId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [ColNumber(7)]
        public string Status { get; set; }
        /// <summary>
        /// 开立时间
        /// </summary>
        [ColNumber(8)]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// 护士审核时间
        /// </summary>
        [ColNumber(9)]
        public DateTime? NurseReviewTime { set; get; }
        /// <summary>
        /// 护士执行时间
        /// </summary>
        [ColNumber(10)]
        public DateTime? NurseExecuteTime { set; get; }
        /// <summary>
        /// 用量
        /// </summary>
        [ColNumber(11)]
        public string Dosage { get; set; }
        /// <summary>
        /// 用量单位
        /// </summary>
        [ColNumber(12)]
        public string Unit { set; get; }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        [ColNumber(13)]
        public string Name { set; get; }
        /// <summary>
        /// 医嘱备注
        /// </summary>
        [ColNumber(14)]
        public string Remark { get; set; }
        /// <summary>
        /// 执行病区
        /// </summary>
        [ColNumber(15)]
        public string HosWardId { set; get; }
    }
}
