using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using WiGeek.Domain.OrderAggregate;

namespace WiGeek.Infrastructure.Repositories
{
    public interface IOrderDapperRepository
    {
        Task BulkCreatAsync(IList<Order> orders);
    }
}