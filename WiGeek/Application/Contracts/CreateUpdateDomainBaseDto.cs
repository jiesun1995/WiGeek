using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdateDomainBaseDto<T> : IHospitalId
    {
        public string HospitalCode { get ; set ; }
        public string HospitalId { get ; set ; }
    }
}
