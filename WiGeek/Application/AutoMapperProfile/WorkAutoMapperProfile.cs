using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.WorkAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class WorkAutoMapperProfile : Profile
    {
        public WorkAutoMapperProfile()
        {
            CreateMap<Work, CreateUpdateWorkDto>();
            CreateMap<Work, WorkDto>();
        }
    }
}
