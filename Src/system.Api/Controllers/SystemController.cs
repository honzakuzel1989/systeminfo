using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Core.Services;
using System.Threading.Tasks;
using systeminfo.Core.Services;

namespace system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly ISystemInfoProvider _systemInfoProvider;
        private readonly IUsageInfoProvider _usageInfoProvider;
        private readonly INetworkInfoProvider _networkInfoProvider;
        private readonly IUpdatesInfoProvider _updatesInfoProvider;

        public SystemController(ILogger<SystemController> logger,
            ISystemInfoProvider systemInfoProvider,
            IUsageInfoProvider usageInfoProvider,
            INetworkInfoProvider networkInfoProvider,
            IUpdatesInfoProvider updatesInfoProvider)
        {
            _logger = logger;
            _systemInfoProvider = systemInfoProvider;
            _usageInfoProvider = usageInfoProvider;
            _networkInfoProvider = networkInfoProvider;
            _updatesInfoProvider = updatesInfoProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var sysnfo = await _systemInfoProvider.Get();
            return new JsonResult(new 
            {
                CpuUsage = sysnfo.UsageInfo.CpuInfo.Usage.Value,
                MemUsage = sysnfo.UsageInfo.MemoryInfo.Usage.Value,
                DiskUsage = sysnfo.UsageInfo.DiskInfo.Usage.Value,
                NetworkInfo = sysnfo.NetworkInfo,
                UpdatesInfo = sysnfo.UpdatesInfo
            });
        }

        [HttpGet("usage")]
        public async Task<IActionResult> GetUsage(string fs)
        {
            var usnfo = await _usageInfoProvider.GetUsageInfo(fs);
            return new JsonResult(new
            {
                CpuUsage = usnfo.CpuInfo.Usage.Value,
                MemUsage = usnfo.MemoryInfo.Usage.Value,
                DiskUsage = usnfo.DiskInfo.Usage.Value,
            });
        }

        [HttpGet("network")]
        public async Task<IActionResult> GetNetworkInfo(string iface)
        {
            var netinfo = await _networkInfoProvider.GetNetworkInfo(iface);
            return new JsonResult(netinfo);
        }

        [HttpGet("updates")]
        public async Task<IActionResult> GetUpdatesInfo()
        {
            var upinfo = await _updatesInfoProvider.GetUpdatesInfo();
            return new JsonResult(upinfo);
        }
    }
}
