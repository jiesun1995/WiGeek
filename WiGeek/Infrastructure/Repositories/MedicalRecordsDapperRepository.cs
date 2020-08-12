using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.EntityFrameworkCore;

namespace WiGeek.Infrastructure.Repositories
{
    public class MedicalRecordsDapperRepository : DapperRepository<WiGeekDbContext>, IMedicalRecordsDapperRepository, IScopedDependency
    {
        public MedicalRecordsDapperRepository(IDbContextProvider<WiGeekDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task BulkCreatAsync(IList<MedicalRecords> medicalRecords)
        {
            await DbConnection.InsertAsync(medicalRecords, DbTransaction);
        }
    }
}
