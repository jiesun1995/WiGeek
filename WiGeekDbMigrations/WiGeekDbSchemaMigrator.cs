using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using WiGeek.EntityFrameworkCore;

namespace WiGeekDbMigrations
{
    [Dependency(ReplaceServices = true)]
    public class WiGeekDbSchemaMigrator : IWiGeekDbSchemaMigrator, ITransientDependency
    {
        private readonly WiGeekDbContext _dbContext;

        public WiGeekDbSchemaMigrator(
            WiGeekDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext
                .Database.MigrateAsync();
        }
    }
}
