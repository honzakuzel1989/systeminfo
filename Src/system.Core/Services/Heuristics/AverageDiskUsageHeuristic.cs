using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public class AverageDiskUsageHeuristic : IDiskUsageHeuristic
    {
        public DiskUsageInfo GetUsageInfo(params DiskMetrics[] metrics)
        {
            return new DiskUsageInfo(metrics.Average(m => m.UsedPerc.Value));
        }
    }
}
