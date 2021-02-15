using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services
{
    public interface IDiskMetricsProvider
    {
        Task<DiskMetrics> GetDiskMetrics(string fs);
    }
}