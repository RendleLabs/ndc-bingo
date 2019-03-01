using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NdcBingo.ClrMetrics;
using NdcBingo.Data;
using NdcBingo.Services;

namespace NdcBingo
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
            services.AddDbContextPool<BingoContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Bingo"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IDataCookies, DataCookies>();
            services.AddSingleton<IPlayerIdGenerator, PlayerIdGenerator>();
            services.AddScoped<IPlayerData, PlayerData>();
            services.AddScoped<ISquareData, SquareData>();
            services.AddHostedService<ClrMetricsService>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMetrics();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }

    public static class Constants
    {
        public const int SquareCount = 16;
        public const int SquaresPerLine = 4;
    }
}
