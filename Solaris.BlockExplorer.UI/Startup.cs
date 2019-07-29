using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solaris.BlockExplorer.DataAccess.Models;
using Solaris.BlockExplorer.UI.Services;

namespace Solaris.BlockExplorer.UI
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
            services
                .AddScoped<IBlockService, BlockService>()
                .AddScoped<ITransactionService, TransactionService>()
                .AddScoped<IAddressService, AddressService>()
                .AddScoped<ICoinDataService, CoinDataService>()
                .AddDbContext<SolarisExplorerContext>((provider, builder) =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var connectionString = configuration.GetConnectionString("SolarisExplorerDatabase");

                    builder.UseSqlServer(connectionString);
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpClient("CoinGecko", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var section = configuration.GetSection("CoinData");
                var url = section.GetValue<string>("Url");
                var coinSelector = section.GetValue<string>("CoinName");
                var baseAddress = url + coinSelector;

                client.BaseAddress= new Uri(baseAddress);
            });
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
            app.UseCookiePolicy();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
