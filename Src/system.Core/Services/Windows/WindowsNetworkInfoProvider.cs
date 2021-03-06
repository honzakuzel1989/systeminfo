﻿using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class WindowsNetworkInfoProvider : INetworkInfoProvider
    {
        private readonly ILogger<WindowsNetworkInfoProvider> _logger;

        public WindowsNetworkInfoProvider(ILogger<WindowsNetworkInfoProvider> logger)
        {
            _logger = logger;
        }

        public Task<NetworkInfo> GetNetworkInfo(string iface)
        {
            return Task.FromResult(new NetworkInfo(iface, "who knows"));
        }
    }
}
