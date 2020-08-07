using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.WardAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class WardAutoMapperProfile : Profile
    {
        public WardAutoMapperProfile()
        {
            CreateMap<Ward, CreateUpdateWardDto>();
            CreateMap<Ward, WardDto>();
        }
    }
}
