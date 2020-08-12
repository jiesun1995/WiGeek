using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using WiGeek.Domain.SeedWork;

namespace WiGeek.Application
{
    public interface IReadFileDataService:IScopedDependency
    {
        void ReadFileWriteData();
    }
}
