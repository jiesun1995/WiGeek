using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace WiGeek.EntityFrameworkCore
{
    [DependsOn(
       typeof(WiGeekDbEntityFrameworkModule)
       )]
    public class WiGeekDbEntityFrameworkDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<WiGeekDbContext>();
        }
    }
}
