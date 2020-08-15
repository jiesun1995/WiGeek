using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using WiGeek.Domain.ValueObject;
using WiGeek.EntityFrameworkCore;

namespace WiGeek.Infrastructure.Repositories
{
    public class DiagnosesDapperRepository : DapperRepository<WiGeekDbContext>, IDiagnosesDapperRepository, IScopedDependency
    {
        public DiagnosesDapperRepository(IDbContextProvider<WiGeekDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async Task<IEnumerable<Diagnosis>> GetNotInMedicalRecords()
        {
            return await DbConnection.QueryAsync<Diagnosis>("select * from Diagnoses where HospitalCode not in ( select HospitalCode from MedicalRecords)");
        }
        public async Task<int> DelNotInMedicalRecords()
        {
            return await DbConnection.ExecuteAsync("update Diagnoses set IsDel = 1 where HospitalCode not in (select HospitalCode from MedicalRecords)");
        }
    }
}
