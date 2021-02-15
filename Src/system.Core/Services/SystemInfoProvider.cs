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

        private const string FILESYSTEM_DEFAULT = "/dev/sda1";
        private const string INTERFACE_DEFAULT = "enp0s25";

        private readonly ILogger<SystemInfoProvider> _logger;
        private readonly IMemoryMetricsProvider _memoryMatricsProvider;
        private readonly IMemoryUsageHeuristic _memoryUsageHeuristic;
        private readonly ICpuMetricsProvider _cpuMetricsProvider;
        private readonly ICpuUsageHeuristic _cpuUsageHeuristic;
        private readonly IDiskMetricsProvider _diskMetricsProvider;
        private readonly IDiskUsageHeuristic _diskUsageHeuristic;
        private readonly INetworkInfoProvider _networkInfoProvider;
        private readonly IMemoryCache _memoryCache;

        public SystemInfoProvider(ILogger<SystemInfoProvider> logger,
            IMemoryMetricsProvider memoryMatricsProvider,
            IMemoryUsageHeuristic memoryUsageHeuristic,
            ICpuMetricsProvider cpuMetricsProvider,
            ICpuUsageHeuristic cpuUsageHeuristic,
            IDiskMetricsProvider diskMetricsProvider,
            IDiskUsageHeuristic diskUsageHeuristic,
            INetworkInfoProvider networkInfoProvider,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryMatricsProvider = memoryMatricsProvider;
            _memoryUsageHeuristic = memoryUsageHeuristic;
            _cpuMetricsProvider = cpuMetricsProvider;
            _cpuUsageHeuristic = cpuUsageHeuristic;
            _diskMetricsProvider = diskMetricsProvider;
            _diskUsageHeuristic = diskUsageHeuristic;
            _networkInfoProvider = networkInfoProvider;
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

            var fs = Environment.GetEnvironmentVariable("SYSTEM_INFO_FILESYSTEM")
                ?? FILESYSTEM_DEFAULT;

            var disk = await _diskMetricsProvider.GetDiskMetrics(fs);

            var iface = Environment.GetEnvironmentVariable("SYSTEM_INFO_INTERFACE")
                ?? INTERFACE_DEFAULT;

            var network = await _networkInfoProvider.GetNetworkInfo(iface);

            return new SystemInfo(
                _cpuUsageHeuristic.GetUsageInfo(lcpu, cpu),
                _memoryUsageHeuristic.GetUsageInfo(lmem, mem),
                _diskUsageHeuristic.GetUsageInfo(disk),
                network);
        }
    }
}
