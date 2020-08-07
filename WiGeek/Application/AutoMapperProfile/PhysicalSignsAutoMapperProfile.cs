using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.ValueObject;

namespace WiGeek.Application.AutoMapperProfile
{
    public class PhysicalSignsAutoMapperProfile : Profile
    {
        public PhysicalSignsAutoMapperProfile()
        {
            CreateMap<PhysicalSigns, CreateUpdatePhysicalSignsDto>();
            CreateMap<PhysicalSigns, PhysicalSignsDto>();
        }
    }
}
