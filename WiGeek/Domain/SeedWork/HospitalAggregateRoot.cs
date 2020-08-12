using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectMapping;
using WiGeek.Infrastructure.Attributes;
using WiGeek.Startup;

namespace WiGeek.Domain.SeedWork
{
    public abstract class HospitalAggregateRoot : Entity<int>, IHospitalId
    { 
        [ColNumber(0)]
        public virtual string HospitalCode { set; get; }
        public string HospitalId { get ; set ; }
    }
}
