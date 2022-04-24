using System;
using WebCast.Forecast.Domain.Common.CustomExceptions;
using WebCast.Forecast.Domain.Forecast.Entities;
using Xunit;

namespace WebCast.Forecast.UnitTest
{
    public class WeatherForecastTests
    {

        [Fact]
        public void WeatherForecastDateCantBeInPast()
        {
            var pastDate = DateTime.Now.AddDays(-1);
            Action act = () => new WeatherForecast(-60, pastDate);
            DomainLogicException exception = Assert.Throws<DomainLogicException>(act);
            Assert.Equal(ErrorConstants.PAST_FOREACAST_DATE, exception.Message);
        }

        [Fact]
        public void WeatherForecastDateCanBeInFuture()
        {
            var futureDate = DateTime.Now.AddDays(1);
            var forecast = new WeatherForecast(-60, futureDate);
            Assert.Equal(forecast.Date, futureDate);
        }

        [Fact]
        public void WeatherForecastTemperatureCantBeBiggerThan60()
        {
            var temperature = 61;
            Action act = () => new WeatherForecast(temperature, DateTime.Now);
            DomainLogicException exception = Assert.Throws<DomainLogicException>(act);
            Assert.Equal(ErrorConstants.INVALID_TEMPERATURE, exception.Message);
        }

        [Fact]
        public void WeatherForecastTemperatureCantBeLesserThan60Negative()
        {
            var temperature = -61;
            Action act = () => new WeatherForecast(temperature, DateTime.Now);
            DomainLogicException exception = Assert.Throws<DomainLogicException>(act);
            Assert.Equal(ErrorConstants.INVALID_TEMPERATURE, exception.Message);
        }


        [Fact]
        public void WeatherSummaryShouldBeFreezing()
        {
            var weatherForecast = new WeatherForecast(-60, DateTime.Now);
            var expected = "Freezing";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeBracing()
        {
            var weatherForecast = new WeatherForecast(-1, DateTime.Now);
            var expected = "Bracing";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WeatherSummaryShouldBeChilly()
        {
            var weatherForecast = new WeatherForecast(7, DateTime.Now);
            var expected = "Chilly";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeCool()
        {
            var weatherForecast = new WeatherForecast(12, DateTime.Now);
            var expected = "Cool";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeMild()
        {
            var weatherForecast = new WeatherForecast(17, DateTime.Now);
            var expected = "Mild";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeWarm()
        {
            var weatherForecast = new WeatherForecast(22, DateTime.Now);
            var expected = "Warm";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeBalmy()
        {
            var weatherForecast = new WeatherForecast(25, DateTime.Now);
            var expected = "Balmy";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeHot()
        {
            var weatherForecast = new WeatherForecast(32, DateTime.Now);
            var expected = "Hot";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeSweltering()
        {
            var weatherForecast = new WeatherForecast(42, DateTime.Now);
            var expected = "Sweltering";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void WeatherSummaryShouldBeScorching()
        {
            var weatherForecast = new WeatherForecast(60, DateTime.Now);
            var expected = "Scorching";

            var result = weatherForecast.GetTemperatureSummary();
            Assert.Equal(expected, result);
        }
    }
}
