using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.Domain.Factories;
using Solaris.BlockExplorer.Domain.Mappings;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.UI.Mappings;
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
            var mapperConfiguration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new DomainObjectsProfile());
                configurationExpression.AddProfile(new PresentationObjectsProfile());
            });
            services.AddSingleton(provider => mapperConfiguration.CreateMapper());

            services
                .AddScoped<IBlockRepository, BlockRepository>()
                .AddScoped<IBlockService, BlockService>()
                .AddScoped<IBlockModelService, BlockModelService>()
                .AddScoped<IBlockTransactionRepository, BlockTransactionRepository>()
                .AddScoped<IBlockTransactionService, BlockTransactionService>()
                .AddScoped<IBlockTransactionModelService, BlockTransactionModelService>()
                .AddScoped<ICoinDataService, CoinDataService>()
                .AddScoped<ITransactionRepository, TransactionRepository>()
                .AddScoped<ITransactionService, TransactionService>()
                .AddScoped<ITransactionModelService, TransactionModelService>()
                .AddScoped<ITransactionInputRepository, TransactionInputRepository>()
                .AddScoped<ITransactionInputService, TransactionInputService>()
                .AddScoped<ITransactionInputModelService, TransactionInputModelService>()
                .AddScoped<ITransactionOutputRepository, TransactionOutputRepository>()
                .AddScoped<ITransactionOutputService, TransactionOutputService>()
                .AddScoped<ITransactionOutputModelService, TransactionOutputModelService>()
                .AddSingleton<IDbConnectionFactory>(provider => new DbConnectionFactory {ConnectionString = Configuration.GetConnectionString("SolarisExplorerDatabase")})
                .AddScoped(provider =>
                {
                    var dbConnectionFactory = provider.GetService<IDbConnectionFactory>();
                    return dbConnectionFactory.CreateConnection();
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpClient("CoinGecko", (provider, client) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var section = configuration.GetSection("CoinData");
                var url = section.GetValue<string>("Url");
                var coinName = section.GetValue<string>("CoinName");
                var baseAddress = url + coinName;

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
