using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts;
using WiGeek.Domain.MedicalRecordsAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class MedicalRecordsAutoMapperProfile : Profile
    {
        public MedicalRecordsAutoMapperProfile()
        {
            //CreateMap<MedicalRecords, CreateUpdateMedicalRecordsDto>();
            CreateMap<CreateUpdateMedicalRecordsDto, MedicalRecords>();
            CreateMap<MedicalRecords, MedicalRecordsDto>();
        }
    }
}
