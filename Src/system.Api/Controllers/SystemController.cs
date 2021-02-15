using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Core.Services;
using System.Linq;
using System.Threading.Tasks;

namespace system.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ILogger<SystemController> _logger;
        private readonly ISystemInfoProvider _systemInfoProvider;

        public SystemController(ILogger<SystemController> logger,
            ISystemInfoProvider systemInfoProvider)
        {
            _logger = logger;
            _systemInfoProvider = systemInfoProvider;
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
                CpuUsage = sysnfo.CpuInfo.Usage.Value,
                MemUsage = sysnfo.MemoryInfo.Usage.Value,
                DiskUsage = sysnfo.DiskInfo.Usage.Value,
                NetworkInfo = sysnfo.NetworkInfo
            });
        }
    }
}
