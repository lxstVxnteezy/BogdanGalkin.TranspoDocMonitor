using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TranspoDocMonitor.Service.Core.Exception;
using TranspoDocMonitor.Service.Middlewares.Exceptions;

namespace TranspoDocMonitor.Service.Middlewares
{
    public class ExceptionHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");


            ExceptionResponse response = exception switch
            {
                OwnException ownException => new ExceptionResponse(
                    HttpStatusCode.UnprocessableEntity,
                    ownException.Code,
                    ownException.UserMessage,
                    ownException.InnerMessage),

                ApplicationException _ => new ExceptionResponse(
                    HttpStatusCode.BadRequest,
                    ReservedCodeError.UnexpectedError,
                    "Application exception occurred."),

                UnauthorizedAccessException _ => new ExceptionResponse(
                    HttpStatusCode.Unauthorized,
                    ReservedCodeError.UnexpectedError,
                    "Unauthorized"),

                _ => new ExceptionResponse(
                    HttpStatusCode.InternalServerError,
                    ReservedCodeError.UnexpectedError,
                    "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await JsonSerializer.SerializeAsync(context.Response.Body, response);

        }
    }

}
