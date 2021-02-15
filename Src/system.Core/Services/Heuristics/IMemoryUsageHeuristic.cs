using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public interface IMemoryUsageHeuristic
    {
        MemoryUsageInfo GetUsageInfo(params MemoryMetrics[] metrics);
    }
}