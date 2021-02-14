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
        private readonly IMemoryMetricsProvider _memoryMatricsProvider;
        private readonly ICpuMetricsProvider _cpuMetricsProvider;

        public SystemInfoProvider(ILogger<SystemInfoProvider> logger,
            IMemoryMetricsProvider memoryMatricsProvider,
            ICpuMetricsProvider cpuMetricsProvider)
        {
            _logger = logger;
            _memoryMatricsProvider = memoryMatricsProvider;
            _cpuMetricsProvider = cpuMetricsProvider;
        }

        public async Task<SystemInfo> Get()
        {
            _logger.LogInformation("Getting system informations...");

            var mem = await _memoryMatricsProvider.GetMemoryMetrics();
            var cpu = await _cpuMetricsProvider.GetCpuMetrics();


            return new SystemInfo(
                new CpuUsageInfo(cpu.UsedPerc),
                new MemoryUsageInfo(mem.UsedPerc),
                new DiskUsageInfo(0));
        }
    }
}
