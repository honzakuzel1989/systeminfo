using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Heuristics
{
    public class AverageMemoryUsageHeuristic : IMemoryUsageHeuristic
    {
        public MemoryUsageInfo GetUsageInfo(params MemoryMetrics[] metrics)
        {
            return new MemoryUsageInfo(metrics.Average(m => m.UsedPerc.Value));
        }
    }
}
