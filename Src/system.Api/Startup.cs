using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Core.Services;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using systeminfo.Core.Services;
using systeminfo.Core.Services.Heuristics;
using systeminfo.Core.Services.Unix;
using systeminfo.Core.Services.Windows;

namespace system
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISystemInfoProvider, SystemInfoProvider>();
            services.AddTransient<ICpuUsageHeuristic, AverageCpuUsageHeuristic>();
            services.AddTransient<IMemoryUsageHeuristic, AverageMemoryUsageHeuristic>();
            services.AddTransient<IDiskUsageHeuristic, AverageDiskUsageHeuristic>();

            if (IsUnix())
            {
                services.AddTransient<IMemoryMetricsProvider, UnixMemoryMetricsProvider>();
                services.AddTransient<ICpuMetricsProvider, UnixCpuMetricsProvider>();
                services.AddTransient<IDiskMetricsProvider, UnixDiskMetricsProvider>();
                services.AddTransient<INetworkInfoProvider, UnixNetworkInfoProvider>();
            }
            else
            {
                services.AddTransient<IMemoryMetricsProvider, WindowsMemoryMetricsProvider>();
                services.AddTransient<ICpuMetricsProvider, WindowsCpuMetricsProvider>();
                services.AddTransient<IDiskMetricsProvider, WindowsDiskMetricsProvider>();
                services.AddTransient<INetworkInfoProvider, WindowsNetworkInfoProvider>();
            }

            services.AddMemoryCache();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private bool IsUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            return isUnix;
        }
    }
}
