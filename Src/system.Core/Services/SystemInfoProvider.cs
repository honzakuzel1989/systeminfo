using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using systeminfo.Core.Entities;
using systeminfo.Core.Services;

namespace System.Core.Services
{
    public class SystemInfoProvider : ISystemInfoProvider
    {
        private const string SYSTEM_INFO_CPU_KEY = "systeminfocpukey";
        private const string SYSTEM_INFO_MEM_KEY = "systeminfomemkey";

        private const string FILESYSTEM_DEFAULT = "/dev/sda1";
        private const string INTERFACE_DEFAULT = "enp0s25";

        private readonly ILogger<SystemInfoProvider> _logger;
        private readonly IUsageInfoProvider _usageInfoProvider;
        private readonly INetworkInfoProvider _networkInfoProvider;
        private readonly IUpdatesInfoProvider _updatesInfoProvider;
        private readonly IMemoryCache _memoryCache;

        public SystemInfoProvider(ILogger<SystemInfoProvider> logger,
            IUsageInfoProvider usageInfoProvider,
            INetworkInfoProvider networkInfoProvider,
            IUpdatesInfoProvider updatesInfoProvider,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _usageInfoProvider = usageInfoProvider;
            _networkInfoProvider = networkInfoProvider;
            _updatesInfoProvider = updatesInfoProvider;
            _memoryCache = memoryCache;
        }

        public async Task<SystemInfo> Get()
        {
            _logger.LogInformation("Getting all system informations...");

            var iface = Environment.GetEnvironmentVariable("SYSTEM_INFO_INTERFACE")
                ?? INTERFACE_DEFAULT;

            var network = await _networkInfoProvider.GetNetworkInfo(iface);
            var packages = await _updatesInfoProvider.GetUpdatesInfo();

            var fs = Environment.GetEnvironmentVariable("SYSTEM_INFO_FILESYSTEM")
                ?? FILESYSTEM_DEFAULT;

            return new SystemInfo(
                await _usageInfoProvider.GetUsageInfo(fs),
                network,
                packages);
        }
    }
}
