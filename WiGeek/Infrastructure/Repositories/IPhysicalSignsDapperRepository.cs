using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.ValueObject;

namespace WiGeek.Infrastructure.Repositories
{
    public interface IPhysicalSignsDapperRepository
    {
        Task BulkCreatAsync(IList<PhysicalSigns> physicalSigns);
    }
}
