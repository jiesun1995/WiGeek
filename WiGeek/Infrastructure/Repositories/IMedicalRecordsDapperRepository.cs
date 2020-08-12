using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiGeek.Domain.MedicalRecordsAggregate;

namespace WiGeek.Infrastructure.Repositories
{
    public interface IMedicalRecordsDapperRepository
    {
        Task BulkCreatAsync(IList<MedicalRecords> medicalRecords);
    }
}
