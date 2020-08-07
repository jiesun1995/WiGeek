using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace WiGeek.Startup
{
        [Dependency(ReplaceServices = true)]
        public class WiGeekBrandingProvider : DefaultBrandingProvider
        {
            public override string AppName => "卫数";
        }   
}
