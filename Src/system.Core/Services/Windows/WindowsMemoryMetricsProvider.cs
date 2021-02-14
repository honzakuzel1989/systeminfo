using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class WindowsMemoryMetricsProvider : IMemoryMatricsProvider
    {
        private readonly ILogger<WindowsMemoryMetricsProvider> _logger;

        public WindowsMemoryMetricsProvider(ILogger<WindowsMemoryMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<MemoryMetrics> GetMemoryMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo();
            info.FileName = "wmic";
            info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = await process.StandardOutput.ReadToEndAsync();

                _logger.LogInformation($"Obtained external proccess output");
                _logger.LogInformation(output);
            }

            var lines = output.Trim().Split("\n");
            var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);

            var total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            var free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
            var used = total - free;

            return new MemoryMetrics(total, free, used);
        }
    }
}
