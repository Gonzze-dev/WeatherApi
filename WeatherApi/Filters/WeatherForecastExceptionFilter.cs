using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeatherApi.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WeatherForecastExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadHttpRequestException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message)
                {
                    StatusCode = 400
                };
                return;
            }

            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = 500    
            };

            return;
        }

    }
}
