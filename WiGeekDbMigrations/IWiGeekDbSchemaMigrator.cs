using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiGeekDbMigrations
{
    public interface IWiGeekDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
