using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Caching;
using WiGeek.Application.Contracts.Report;

namespace WiGeek
{
    public class ReportHub:AbpHub
    {

        private readonly IDistributedCache<List<DReportDto>> _dReportcache;

        public ReportHub(IDistributedCache<List<DReportDto>> dReportcache)
        {
            _dReportcache = dReportcache;
        }

        public async Task RefreshDReport()
        {
            var dReportDtos = _dReportcache.GetOrAdd(nameof(DReportDto), () =>
            {
                return new List<DReportDto>();
            });
            await Clients.All.SendAsync("RefreshDReport", dReportDtos);
        }

    }
}
