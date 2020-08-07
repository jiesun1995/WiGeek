using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateWorkDto : CreateUpdateDomainBaseDto<WorkDto>
    {
        /// <summary>
        /// 工作编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 工作名称
        /// </summary>
        public string Name { get; set; }
    }
}
