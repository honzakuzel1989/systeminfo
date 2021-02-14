using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services
{
    public interface IMemoryMetricsProvider
    {
        Task<MemoryMetrics> GetMemoryMetrics();
    }
}