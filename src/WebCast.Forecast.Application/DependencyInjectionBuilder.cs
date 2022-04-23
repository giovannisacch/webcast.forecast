using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Interfaces.Infra;
using WebCast.Forecast.Infra;

namespace WebCast.Forecast.Application
{
    public static class DependencyInjectionBuilder
    {
        public static void InjectInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetSection("RedisSettings")["ConnectionString"];
                options.InstanceName = "webcast";
            });
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        }
    }
}
