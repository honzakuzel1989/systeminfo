using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Core.Entities;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using systeminfo.Core.Services;

namespace System.Core.Services
{
    public class SystemInfoProvider : ISystemInfoProvider
    {
        private readonly ILogger<SystemInfoProvider> _logger;
        private readonly IMemoryMatricsProvider _memoryMatricsProvider;

        public SystemInfoProvider(ILogger<SystemInfoProvider> logger,
            IMemoryMatricsProvider memoryMatricsProvider)
        {
            _logger = logger;
            _memoryMatricsProvider = memoryMatricsProvider;
        }

        public async Task<SystemInfo> Get()
        {
            _logger.LogInformation("Getting system informations...");

            var mm = await _memoryMatricsProvider.GetMemoryMetrics();

            return new SystemInfo(
                new CpuUsageInfo(0),
                new MemoryUsageInfo(mm.UsedPerc),
                new DiskUsageInfo(0));
        }
    }
}
