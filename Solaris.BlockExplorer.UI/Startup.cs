using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Solaris.BlockExplorer.DataAccess.Repositories;
using Solaris.BlockExplorer.DataAccess.Repositories.Api;
using Solaris.BlockExplorer.Domain.Factories;
using Solaris.BlockExplorer.Domain.Mappings;
using Solaris.BlockExplorer.Domain.Services;
using Solaris.BlockExplorer.Domain.Services.Api;
using Solaris.BlockExplorer.UI.Mappings;
using Solaris.BlockExplorer.UI.Services;
using Solaris.BlockExplorer.UI.Services.Api;

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
                .AddScoped<DataAccess.Repositories.IBlockRepository, DataAccess.Repositories.BlockRepository>()
                .AddScoped<Domain.Services.IBlockService, Domain.Services.BlockService>()
                .AddScoped<Services.IBlockModelService, Services.BlockModelService>()
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
                .AddScoped<DataAccess.Repositories.IAddressRepository, DataAccess.Repositories.AddressRepository>()
                .AddScoped<Domain.Services.IAddressService, Domain.Services.AddressService>()
                .AddScoped<Services.IAddressModelService, Services.AddressModelService>()
                .AddScoped<ISearchRepository, SearchRepository>()
                .AddScoped<ISearchService, SearchService>()
                .AddScoped<ISearchModelService, SearchModelService>()
                .AddScoped<ICurrentTotalSupplyRepository, CurrentTotalSupplyRepository>()
                .AddScoped<ICurrentTotalSupplyService, CurrentTotalSupplyService>()
                .AddScoped<ICurrentTotalSupplyModelService, CurrentTotalSupplyModelService>()
                .AddScoped<IDifficultyRepository, DifficultyRepository>()
                .AddScoped<IDifficultyService, DifficultyService>()
                .AddScoped<IDifficultyModelService, DifficultyModelService>()
                .AddScoped<IBlockHashRepository, BlockHashRepository>()
                .AddScoped<IBlockHashService, BlockHashService>()
                .AddScoped<IBlockHashModelService, BlockHashModelService>()
                .AddScoped<DataAccess.Repositories.Api.IBlockRepository, DataAccess.Repositories.Api.BlockRepository>()
                .AddScoped<Domain.Services.Api.IBlockService, Domain.Services.Api.BlockService>()
                .AddScoped<Services.Api.IBlockModelService, Services.Api.BlockModelService>()
                .AddScoped<IRawTransactionRepository, RawTransactionRepository>()
                .AddScoped<IRawTransactionService, RawTransactionService>()
                .AddScoped<IRawTransactionModelService, RawTransactionModelService>()
                .AddScoped<IMoneySupplyRepository, MoneySupplyRepository>()
                .AddScoped<IMoneySupplyService, MoneySupplyService>()
                .AddScoped<IMoneySupplyModelService, MoneySupplyModelService>()
                .AddScoped<DataAccess.Repositories.Api.IAddressRepository, DataAccess.Repositories.Api.AddressRepository>()
                .AddScoped<Domain.Services.Api.IAddressService, Domain.Services.Api.AddressService>()
                .AddScoped<Services.Api.IAddressModelService, Services.Api.AddressModelService>()
                .AddScoped<IBalanceRepository, BalanceRepository>()
                .AddScoped<IBalanceService, BalanceService>()
                .AddScoped<IBalanceModelService, BalanceModelService>()
                .AddScoped<IAddressTransactionRepository, AddressTransactionRepository>()
                .AddScoped<IAddressTransactionService, AddressTransactionService>()
                .AddScoped<IAddressTransactionModelService, AddressTransactionModelService>()
                .AddScoped<IRichListRepository, RichListRepository>()
                .AddScoped<IRichListService, RichListService>()
                .AddScoped<IRichListModelService, RichListModelService>()
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
                var section = configuration.GetSection("CoinGecko");
                var url = section.GetValue<string>("Url");
                var coinName = section.GetValue<string>("CoinName");
                var baseAddress = url + coinName;

                client.BaseAddress= new Uri(baseAddress);
            });
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts(new HstsOptions(TimeSpan.FromDays(365*2), includeSubDomains: true, preload: true));
            
            app.UseCsp(csp =>
            {
                csp.ByDefaultAllow
                    .FromNowhere();
                csp.AllowScripts
                    .FromSelf().AllowUnsafeInline().AllowUnsafeEval();
                csp.AllowStyles
                    .FromSelf().AllowUnsafeInline();
                csp.AllowImages
                    .FromSelf();
                csp.AllowAudioAndVideo
                    .FromNowhere();
                csp.AllowFrames
                    .FromNowhere();
                csp.AllowConnections
                    .ToSelf();
                csp.AllowFonts
                    .FromSelf();
                csp.AllowPlugins
                    .FromNowhere();
                csp.AllowFraming
                    .FromNowhere();
                csp.AllowBaseUri
                    .FromNowhere();
                csp.AllowFormActions
                    .ToSelf();
            });
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("x-frame-options", "DENY");
                context.Response.Headers.Add("x-xss-protection", "1; mode=block");
                context.Response.Headers.Add("x-content-type-options", "nosniff");
                context.Response.Headers.Add("cache-control", "no-cache, no-store, max-age=0, must-revalidate");
                context.Response.Headers.Add("pragma", "no-cache");
                context.Response.Headers.Add("expires", "0");
                context.Response.Headers.Add("referrer-policy", "same-origin");

                await next();
            });

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US")
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("en")
                },
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
            });

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
                routes.MapRoute(
                    name: "Address",
                    template: "Address/{Id}");
                routes.MapRoute(
                    name: "Block",
                    template: "Block/{Id}");
                routes.MapRoute(
                    name: "Transaction",
                    template: "Transaction/{Id}");
                routes.MapRoute(
                    name: "ApiGetAddress",
                    template: "api/GetAddress/{PublicKey}");
                routes.MapRoute(
                    name: "ApiGetBalance",
                    template: "api/GetBalance/{PublicKey}");
                routes.MapRoute(
                    name: "ApiGetAddressTransactions",
                    template: "api/getlasttxs/{PublicKey}/{Count}/{Min}");

            });
        }
    }
}
