using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebCast.Forecast.Domain.Forecast.Entities;
using WebCast.Forecast.Domain.Forecast.Interfaces.Infra;

namespace WebCast.Forecast.Infra
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly IDistributedCache _repository;
        private readonly string _aggregateIndexPrefix = "weekAggregate:";
        public WeatherForecastRepository(IDistributedCache repository)
        {
            _repository = repository;
        }
        public async Task AddForecastAsync(WeatherForecast weatherForecast)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration =  weatherForecast.Date.AddTicks(new TimeOnly(23, 59, 59).Ticks)
            };

            var weatherForecastSerliazed = JsonSerializer.Serialize(weatherForecast);

            await _repository.SetStringAsync(GetCacheKeyByWheatherForecastDate(weatherForecast.Date), weatherForecastSerliazed, options);

            if (weatherForecast.Date.CompareTo(DateTime.Now.AddDays(7)) < 0)
                await UpdateNextSevenDaysAggregateIndex();
        }
        public async Task<IEnumerable<WeatherForecast>> GetNextSevenDaysWeatherForecastsAsync()
        {
            var weatherForecastAggregate = await _repository.GetStringAsync(_aggregateIndexPrefix + GetCacheKeyByWheatherForecastDate(DateTime.Now.Date));
            if (string.IsNullOrEmpty(weatherForecastAggregate))
                return await UpdateNextSevenDaysAggregateIndex();

            var result = JsonSerializer.Deserialize<IEnumerable<WeatherForecastDTO>>(weatherForecastAggregate);
            return result.Select(x => new WeatherForecast(x.Temperature, x.Date));
        }

        private async Task<List<WeatherForecast>> GetWeatherForecastsFromNextSevenDaysIndexes()
        {
            var weatherForecasts = new List<WeatherForecast>();
            var nextSevenDaysWeatherForecastsTasks = new Task[7];
            var actualDate = DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                nextSevenDaysWeatherForecastsTasks[i] = GetWeatherForecastAsync(actualDate.AddDays(i));
            }
            await Task.WhenAll(nextSevenDaysWeatherForecastsTasks);

            for (int i = 0; i < nextSevenDaysWeatherForecastsTasks.Length; i++)
            {
                var taskResult = ((Task<WeatherForecast>)nextSevenDaysWeatherForecastsTasks[i]).Result;
                if (taskResult == null)
                    continue;
                weatherForecasts.Add(taskResult);
            }
            return weatherForecasts.OrderBy(x => x.Date).ToList();
        }
        private async Task<IEnumerable<WeatherForecast>> UpdateNextSevenDaysAggregateIndex()
        {
            var actualDate = DateTime.Now.Date;
            var nextSevenDaysIndexes = await GetWeatherForecastsFromNextSevenDaysIndexes();
            if ( !nextSevenDaysIndexes.Exists(x => x != null) )
                return Enumerable.Empty<WeatherForecast>();
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = actualDate.Date.AddTicks(new TimeOnly(23, 59, 59).Ticks)
            };
            var weatherForecastsSerliazed = JsonSerializer.Serialize(nextSevenDaysIndexes);
            await _repository.SetStringAsync(_aggregateIndexPrefix + GetCacheKeyByWheatherForecastDate(actualDate), weatherForecastsSerliazed, options);
            return nextSevenDaysIndexes;
        }
        private async Task<WeatherForecast> GetWeatherForecastAsync(DateTime date)
        {
            var weatherForecast = await _repository.GetStringAsync(GetCacheKeyByWheatherForecastDate(date));
            if (string.IsNullOrEmpty(weatherForecast))
                return null;
            var result = JsonSerializer.Deserialize<WeatherForecastDTO>(weatherForecast);

            return new WeatherForecast(result.Temperature, result.Date);
        }
        private string GetCacheKeyByWheatherForecastDate(DateTime forecastDate)
        {
            return forecastDate.ToShortDateString();
        }
    }
}
