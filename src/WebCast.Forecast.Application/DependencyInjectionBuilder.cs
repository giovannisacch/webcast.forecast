using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebCast.Forecast.Application.Interfaces;
using WebCast.Forecast.Application.Services;
using WebCast.Forecast.Domain.Forecast.Interfaces.Infra;
using WebCast.Forecast.Infra;

namespace WebCast.Forecast.Application
{
    public static class DependencyInjectionBuilder
    {
        public static void InjectApplicationDependencies(this IServiceCollection services)
        {
            
            services.AddScoped<IWeatherForecastApplicationService, WeatherForecastApplicationService>();
        }

        public static void InjectInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("RedisSettings")["ConnectionString"];
            });
            services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
        }
    }
}
