using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebCast.Forecast.Application.Models;
using WebCast.Forecast.Domain.Common.CustomExceptions;

namespace WebCast.Forecast.Application
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainLogicException domainException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseModel(domainException.Message)));
            }
            catch(Exception exception)
            {
                //Log
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponseModel(exception.Message+ "\n Verify your logs: " + DateTime.Now.ToString())));
            }
        }
    }
}
