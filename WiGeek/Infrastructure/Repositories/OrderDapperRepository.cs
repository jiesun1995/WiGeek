using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WiGeek.Domain.OrderAggregate;
using WiGeek.EntityFrameworkCore;

namespace WiGeek.Infrastructure.Repositories
{
    public class OrderDapperRepository : DapperRepository<WiGeekDbContext> ,IOrderDapperRepository, ITransientDependency
    {
        public OrderDapperRepository(IDbContextProvider<WiGeekDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task BulkCreatAsync(IList<Order> orders)
        {
            await DbConnection.InsertAsync(orders);
        }
    }
}
