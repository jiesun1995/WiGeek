using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MarriageAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class MarriageAutoMapperProfile : Profile
    {
        public MarriageAutoMapperProfile()
        {
            CreateMap<Marriage, CreateUpdateMarriageDto>();
            CreateMap<Marriage, MarriageDto>();
        }
    }
}
