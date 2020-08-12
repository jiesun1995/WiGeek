using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using WiGeek.EntityFrameworkCore;

namespace WiGeekDbMigrations
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(WiGeekDbEntityFrameworkDbMigrationsModule)
        )]
    public class WiGeekDbMigratorModule : AbpModule
    {
    }
}
