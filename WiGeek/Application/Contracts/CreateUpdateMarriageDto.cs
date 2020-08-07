using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateMarriageDto : CreateUpdateDomainBaseDto<MarriageDto>
    {
        /// <summary>
        /// 婚姻编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 婚姻名称
        /// </summary>
        public string Name { get; set; }
    }
}
