using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class WindowsDiskMetricsProvider : IDiskMetricsProvider
    {
        private readonly ILogger<WindowsDiskMetricsProvider> _logger;

        public WindowsDiskMetricsProvider(ILogger<WindowsDiskMetricsProvider> logger)
        {
            _logger = logger;
        }

        public Task<DiskMetrics> GetDiskMetrics(string fs)
        {
            return Task.FromResult(new DiskMetrics(
                new Percentage(42),
                0,
                0,
                0,
                fs));
        }
    }
}
