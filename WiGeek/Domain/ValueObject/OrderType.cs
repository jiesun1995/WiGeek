using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.ValueObject
{
    /// <summary>
    /// 医嘱类型
    /// </summary>
    public class OrderType : HospitalAggregateRoot
    {
        /// <summary>
        /// 医嘱类型名称
        /// </summary>
        [ColNumber(1)]
        [MaxLength(500)]
        public string Name { get; set; }
    }
}
