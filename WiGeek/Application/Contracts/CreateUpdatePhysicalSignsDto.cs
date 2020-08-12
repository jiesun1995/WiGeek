using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Infrastructure.Attributes;

namespace WiGeek.Application.Contracts
{
    public class CreateUpdatePhysicalSignsDto: CreateUpdateDomainBaseDto<PhysicalSignsDto>
    {
        public override string HospitalCode { get; set; }
        [ColNumber(0)]
        public string MedicalRecordsId { get; set; }
        [ColNumber(1)]
        public DateTime? CreateTime { get; set; }
        [ColNumber(2)]
        public string Breathe { set; get; }
        [ColNumber(3)]
        public string HeartRate { set; get; }
        [ColNumber(4)]
        public string Temperature { get; set; }
    }
}
