using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Core.Entities;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class UnixCpuMetricsProvider : ICpuMetricsProvider
    {
        private readonly ILogger<UnixCpuMetricsProvider> _logger;

        public UnixCpuMetricsProvider(ILogger<UnixCpuMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<CpuMetrics> GetCpuMetrics()
        {
            var output = "";
            var cmd = "top -b -d1 -n1|grep -i 'Cpu(s)'|xargs|cut -d ' ' -f2";

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

            var used = Math.Round(double.Parse(output.Replace(',', '.'), CultureInfo.InvariantCulture));

            var metrics = new CpuMetrics(new Percentage(used));
            _logger.LogInformation($"Cpu metrics {metrics}");

            return metrics;
        }
    }
}
