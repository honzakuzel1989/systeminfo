using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services
{
    public interface ICpuMetricsProvider
    {
        Task<CpuMetrics> GetCpuMetrics();
    }
}