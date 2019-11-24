using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkerProfileApi.Mappers;

namespace WorkerProfileApi.Middleware
{
    public class ApiExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionToApiErrorMapper _mapper;

        private readonly ILogger _logger;
        public ApiExceptionsHandlingMiddleware(RequestDelegate next, IExceptionToApiErrorMapper mapper, ILogger<ApiExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var eventId = new EventId();
                context.Response.Clear();
                var error = _mapper.map(ex, eventId.ToString());
                context.Response.StatusCode = error.StatusCode;
                context.Response.ContentType = "application/json";
                _logger.LogError(eventId, ex, "");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}