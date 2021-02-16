using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class UnixMemoryMetricsProvider : IMemoryMetricsProvider
    {
        private readonly ILogger<UnixMemoryMetricsProvider> _logger;

        public UnixMemoryMetricsProvider(ILogger<UnixMemoryMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<MemoryMetrics> GetMemoryMetrics()
        {
            var output = "";
            var cmd = "free -m";

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

            var lines = output.Split("\n");
            var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var total = double.Parse(memory[1].Replace(',', '.'), CultureInfo.InvariantCulture);
            var available = double.Parse(memory[6].Replace(',', '.'), CultureInfo.InvariantCulture);
            var used = total - available;

            var metrics = new MemoryMetrics(total, available, used);
            _logger.LogInformation($"Memory metrics {metrics}");

            return metrics;
        }
    }
}
