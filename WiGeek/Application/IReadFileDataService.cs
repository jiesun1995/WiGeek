using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WiGeek.Application
{
    public interface IReadFileDataService:IScopedDependency
    {
        IEnumerable<T> readData<T>(string FilePath) where T : class, new();
    }
}
