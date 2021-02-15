using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using systeminfo.Core.Entities;
using systeminfo.Core.Services;
using systeminfo.Core.Services.Heuristics;

namespace System.Core.Services
{
    public class SystemInfoProvider : ISystemInfoProvider
    {
        private const string SYSTEM_INFO_CPU_KEY = "systeminfocpukey";
        private const string SYSTEM_INFO_MEM_KEY = "systeminfomemkey";

        private readonly ILogger<SystemInfoProvider> _logger;
        private readonly IMemoryMetricsProvider _memoryMatricsProvider;
        private readonly IMemoryUsageHeuristic _memoryUsageHeuristic;
        private readonly ICpuMetricsProvider _cpuMetricsProvider;
        private readonly ICpuUsageHeuristic _cpuUsageHeuristic;
        private readonly IMemoryCache _memoryCache;

        public SystemInfoProvider(ILogger<SystemInfoProvider> logger,
            IMemoryMetricsProvider memoryMatricsProvider,
            IMemoryUsageHeuristic memoryUsageHeuristic,
            ICpuMetricsProvider cpuMetricsProvider,
            ICpuUsageHeuristic cpuUsageHeuristic,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryMatricsProvider = memoryMatricsProvider;
            _memoryUsageHeuristic = memoryUsageHeuristic;
            _cpuMetricsProvider = cpuMetricsProvider;
            _cpuUsageHeuristic = cpuUsageHeuristic;
            _memoryCache = memoryCache;
        }

        public async Task<SystemInfo> Get()
        {
            _logger.LogInformation("Getting system informations...");

            var mem = await _memoryMatricsProvider.GetMemoryMetrics();
            var cpu = await _cpuMetricsProvider.GetCpuMetrics();

            var lcpu = _memoryCache.GetOrCreate(SYSTEM_INFO_CPU_KEY, _ => cpu);
            _memoryCache.Set(SYSTEM_INFO_CPU_KEY, cpu);

            var lmem = _memoryCache.GetOrCreate(SYSTEM_INFO_MEM_KEY, _ => mem);
            _memoryCache.Set(SYSTEM_INFO_MEM_KEY, mem);

            return new SystemInfo(
                _cpuUsageHeuristic.GetUsageInfo(lcpu, cpu),
                _memoryUsageHeuristic.GetUsageInfo(lmem, mem),
                new DiskUsageInfo(0));
        }
    }
}
