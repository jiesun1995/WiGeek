using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Application.Contracts.Report;
using WiGeek.Domain.MedicalRecordsAggregate;

namespace WiGeek.Application.AutoMapperProfile
{
    public class ReportAutoMapperProfile : Profile
    {
        public ReportAutoMapperProfile()
        {
            CreateMap<MedicalRecords, DReportDto>();
        }
    }
}
