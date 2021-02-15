using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public class AverageCpuUsageHeuristic : ICpuUsageHeuristic
    {
        public CpuUsageInfo GetUsageInfo(params CpuMetrics[] metrics)
        {
            return new CpuUsageInfo(metrics.Average(m => m.UsedPerc.Value));
        }
    }
}
