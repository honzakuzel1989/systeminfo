using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class UnixMemoryMetricsProvider : IMemoryMatricsProvider
    {
        private readonly ILogger<UnixMemoryMetricsProvider> _logger;

        public UnixMemoryMetricsProvider(ILogger<UnixMemoryMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<MemoryMetrics> GetMemoryMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo("free -m");
            info.FileName = "/bin/bash";
            info.Arguments = "-c \"free -m\"";
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = await process.StandardOutput.ReadToEndAsync();

                _logger.LogInformation($"Obtained external proccess output");
                _logger.LogInformation(output);
            }

            var lines = output.Split("\n");
            var memory = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var total = double.Parse(memory[1]);
            var available = double.Parse(memory[6]);
            var used = total - available;

            return new MemoryMetrics(total, available, used);
        }
    }
}
