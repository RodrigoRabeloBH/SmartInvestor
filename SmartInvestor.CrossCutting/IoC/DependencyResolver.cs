using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SmartInvestor.Application.Services;
using SmartInvestor.Domain.Interfaces;
using SmartInvestor.Infrastructure.Data;
using SmartInvestor.Infrastructure.Repositories;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SmartInvestor.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static IServiceCollection AddDependencyResolver(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDatabases(services, configuration);

            RegisterRedis(services, configuration);

            RegisterRepositoriesAndServices(services);

            RegisterHttp(services, configuration);

            return services;
        }

        private static void RegisterDatabases(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SmartInvestorDbContext>(opt =>
            {
                opt.UseNpgsql(configuration["SMART_INVESTOR_DB"]);
            });
        }

        private static void RegisterRedis(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(configuration["REDIS_CONNECTION"]);

                return ConnectionMultiplexer.Connect(options);
            });
        }

        private static void RegisterRepositoriesAndServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddScoped<IBrapiServices, BrapiServices>();
        }

        private static void RegisterHttp(IServiceCollection services, IConfiguration configuration)
        {
            var uri = configuration["BRAPI_URI"];
            var token = configuration["BRAPI_TOKEN"];

            services.AddHttpClient(nameof(BrapiServices), c =>
            {
                c.BaseAddress = new Uri(uri);
                c.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }).AddPolicyHandler(GetPolicy());
        }


        private static IAsyncPolicy<HttpResponseMessage> GetPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));
        }
    }
}
