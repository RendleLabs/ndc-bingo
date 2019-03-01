using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.InfluxDB;
using App.Metrics.Reporting.InfluxDB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NdcBingo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // .ConfigureMetricsWithDefaults(
                //     (context, builder) =>
                //     {
                //         builder.Configuration.ReadFrom(context.Configuration);
                //         builder.Report.ToInfluxDb(options =>
                //         {
                //             context.Configuration.GetSection("MetricsOptions").Bind(options);
                //             options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
                //         });
                //     })
                .UseMetrics()
                .UseStartup<Startup>();
    }
}
