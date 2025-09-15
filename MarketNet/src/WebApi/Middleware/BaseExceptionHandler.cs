using System.Text.Json;
using MarketNet.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Diagnostics;

namespace MarketNet.WebApi.Middleware
{
    public class BaseExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BaseException bex)
                return false;

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = bex.StatusCode;
            httpContext.Response.ContentType = "application/json";

            var payload = new { errorType = bex.ErrorType, statusCode = bex.StatusCode, message = bex.Message };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            await httpContext.Response.WriteAsync(json, cancellationToken);
            return true;
        }
    }
}