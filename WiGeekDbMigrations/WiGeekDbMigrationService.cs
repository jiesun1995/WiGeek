using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WiGeekDbMigrations
{
    public class WiGeekDbMigrationService : ITransientDependency
    {
        public ILogger<WiGeekDbMigrationService> Logger { get; set; }

        private readonly IWiGeekDbSchemaMigrator _dbSchemaMigrator;

        public WiGeekDbMigrationService(
            IWiGeekDbSchemaMigrator dbSchemaMigrator)
        {
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<WiGeekDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}
