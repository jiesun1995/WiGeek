﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Domain.ValueObject
{
    public class OrderStatus: AggregateRoot<int>
    {
        [ColNumber(0)]
        public virtual string HospitalCode { set; get; }
        /// <summary>
        /// 状态名称
        /// </summary>
        [ColNumber(1)]
        public string Name { get; set; }
    }
}
