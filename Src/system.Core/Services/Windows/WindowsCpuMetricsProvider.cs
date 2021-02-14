using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class WindowsCpuMetricsProvider : ICpuMetricsProvider
    {
        private readonly ILogger<WindowsCpuMetricsProvider> _logger;

        public WindowsCpuMetricsProvider(ILogger<WindowsCpuMetricsProvider> logger)
        {
            _logger = logger;
        }

        public Task<CpuMetrics> GetCpuMetrics()
        {
            return Task.FromResult(new CpuMetrics(42));
        }
    }
}
