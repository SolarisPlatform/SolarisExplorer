using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddScoped(provider =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var section = configuration.GetSection("MongoDB");
                    var connectionString = section.GetValue<string>("ConnectionString");
                    return new MongoClient(connectionString);
                })
                .AddScoped(provider =>
                {
                    var configuration = provider.GetService<IConfiguration>();
                    var section = configuration.GetSection("MongoDB");
                    var databaseName = section.GetValue<string>("DatabaseName");
                    var mongoClient = provider.GetService<MongoClient>();
                    return mongoClient.GetDatabase(databaseName);
                })
                .AddScoped<IBlockService, BlockService>()
                .AddScoped<ITransactionService, TransactionService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
