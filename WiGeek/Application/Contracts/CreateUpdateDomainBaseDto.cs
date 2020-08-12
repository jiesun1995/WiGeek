using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateDomainBaseDto<T> : IHospitalId
    {
        [ColNumber(0)]
        public virtual string HospitalCode { get ; set ; }
        public string HospitalId { get ; set ; }
    }
}
