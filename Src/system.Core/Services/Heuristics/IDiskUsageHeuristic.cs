using System;
using System.Collections.Generic;
using System.Text;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public interface IDiskUsageHeuristic
    {
        DiskUsageInfo GetUsageInfo(params DiskMetrics[] metrics);
    }
}
