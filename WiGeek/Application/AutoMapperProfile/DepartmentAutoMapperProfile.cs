using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.DepartmentAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class DepartmentAutoMapperProfile : Profile
    {
        public DepartmentAutoMapperProfile()
        {
            CreateMap<Department, CreateUpdateDepartmentDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
