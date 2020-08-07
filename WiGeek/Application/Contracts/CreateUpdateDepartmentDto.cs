using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateDepartmentDto : CreateUpdateDomainBaseDto<DepartmentDto>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string InputCode { get; set; }


    }
}
