using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Windows
{
    public class UnixDiskMetricsProvider : IDiskMetricsProvider
    {
        private readonly ILogger<UnixDiskMetricsProvider> _logger;

        public UnixDiskMetricsProvider(ILogger<UnixDiskMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<DiskMetrics> GetDiskMetrics(string fs)
        {
            var output = "";
            var cmd = string.Format("df | grep '{0}' | xargs", fs);

            var info = new ProcessStartInfo(cmd);
            info.FileName = "/bin/bash";
            info.Arguments = $"-c \"{cmd}\"";
            info.RedirectStandardOutput = true;

            _logger.LogInformation($"Executing cmd {cmd}");
            using (var process = Process.Start(info))
            {
                output = await process.StandardOutput.ReadToEndAsync();

                _logger.LogInformation($"Obtained external proccess output");
                _logger.LogInformation(output);
            }

            var data = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            _logger.LogInformation($"Final values {string.Join(", ", data)}");

            var filesystem = data[0];
            var size = int.Parse(data[1]);
            var used = int.Parse(data[2]);
            var avail = int.Parse(data[3]);
            var use = int.Parse(data[4].TrimEnd('%'));

            var metrics = new DiskMetrics(
                new Percentage(use),
                size,
                used,
                avail, 
                filesystem);

            _logger.LogInformation($"Disk metrics {metrics}");
            return metrics;
        }
    }
}
