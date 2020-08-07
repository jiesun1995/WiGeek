using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiGeek.Domain.SeedWork
{
    public interface IHospitalId
    {
        public string HospitalCode { set; get; }
        public string HospitalId { get; set; }
    }
}
