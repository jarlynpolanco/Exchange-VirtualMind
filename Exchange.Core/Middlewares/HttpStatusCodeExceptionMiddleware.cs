using Exchange.Contracts;
using Exchange.Core.Exceptions;
using Exchange.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Exchange.Core.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _log;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next, ILog log)
        {
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusException ex)
            {
                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new GenericResponse<string>
                {
                    Message = ex.Message,
                    ErrorCode = context.Response.StatusCode.ToString()
                });

                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new GenericResponse<string>
                {
                    Message = ex.Message,
                    ErrorCode = context.Response.StatusCode.ToString()
                });
                _log.WriteLog(ex.Message, LogTypeEnum.ERROR.ToString());
                await context.Response.WriteAsync(result);
            }
        }
    }
}
