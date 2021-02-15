using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class WindowsNetworkInfoProvider : INetworkInfoProvider
    {
        private readonly ILogger<UnixCpuMetricsProvider> _logger;

        public WindowsNetworkInfoProvider(ILogger<UnixCpuMetricsProvider> logger)
        {
            _logger = logger;
        }

        public Task<NetworkInfo> GetNetworkInfo(string iface)
        {
            return Task.FromResult(new NetworkInfo(iface, "who knows"));
        }
    }
}
