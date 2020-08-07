using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateWardDto : CreateUpdateDomainBaseDto<WardDto>
    {
        /// <summary>
        /// 病区名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 过滤码
        /// </summary>
        public string InputCode { get; set; }
    }
}
