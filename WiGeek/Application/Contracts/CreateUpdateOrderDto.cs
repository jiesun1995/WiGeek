using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateOrderDto : CreateUpdateDomainBaseDto<OrderDto>
    {
        public string Type { get; set; }
    }
}
