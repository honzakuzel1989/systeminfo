using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class WindowsUpdatesInfoProvider : IUpdatesInfoProvider
    {
        private readonly ILogger<WindowsUpdatesInfoProvider> _logger;

        public WindowsUpdatesInfoProvider(ILogger<WindowsUpdatesInfoProvider> logger)
        {
            _logger = logger;
        }

        public Task<UpdatesInfo> GetUpdatesInfo()
        {
            return Task.FromResult(new UpdatesInfo(Array.Empty<Package>()));
        }
    }
}
