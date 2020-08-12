using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using WiGeek.Domain.MedicalRecordsAggregate;
using WiGeek.Domain.ValueObject;
using WiGeek.EntityFrameworkCore;

namespace WiGeek.Infrastructure.Repositories
{
    public class PhysicalSignsDapperRepository : DapperRepository<WiGeekDbContext>, IPhysicalSignsDapperRepository, ITransientDependency
    {
        public PhysicalSignsDapperRepository(IDbContextProvider<WiGeekDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task BulkCreatAsync(IList<PhysicalSigns> physicalSigns)
        {
            using (DbTransaction)
            {
                await DbConnection.InsertAsync(physicalSigns, DbTransaction);
            }
        }
    }
}
