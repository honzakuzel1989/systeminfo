using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using systeminfo.Core.Entities;

namespace systeminfo.Core.Services.Unix
{
    public class UnixUpdatesInfoProvider : IUpdatesInfoProvider
    {
        private readonly ILogger<UnixUpdatesInfoProvider> _logger;

        public UnixUpdatesInfoProvider(ILogger<UnixUpdatesInfoProvider> logger)
        {
            _logger = logger;
        }

        public async Task<UpdatesInfo> GetUpdatesInfo()
        {
            var output = "";
            var cmd = "apt list --upgradable";

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

            var pinfos = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var pnames = pinfos.Select(pi => new string(pi.TakeWhile(c => c != '/').ToArray()))
                .Take(pinfos.Length - 1);

            _logger.LogInformation($"Upgradable packages {string.Join(',', pnames)}");
            return new UpdatesInfo(pnames.Select(pn => new Package(pn)).ToArray());
        }
    }
}
