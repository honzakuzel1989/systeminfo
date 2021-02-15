using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public interface ICpuUsageHeuristic
    {
        CpuUsageInfo GetUsageInfo(params CpuMetrics[] metrics);
    }
}