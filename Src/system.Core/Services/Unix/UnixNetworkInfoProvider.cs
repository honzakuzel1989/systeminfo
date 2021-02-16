using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class UnixNetworkInfoProvider : INetworkInfoProvider
    {
        private readonly ILogger<UnixCpuMetricsProvider> _logger;

        public UnixNetworkInfoProvider(ILogger<UnixCpuMetricsProvider> logger)
        {
            _logger = logger;
        }

        public async Task<NetworkInfo> GetNetworkInfo(string iface)
        {
            var output = "";
            var cmd = string.Format("ifconfig | grep {0} -A 1 | grep inet | xargs | cut -d ' ' -f 2", iface);

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

            _logger.LogInformation($"Interface {iface} ip address {output}");
            return new NetworkInfo(output, iface);
        }
    }
}
