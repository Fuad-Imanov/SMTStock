using SMTstock.Entities.Exceptions;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using KeyNotFoundException = SMTstock.Entities.Exceptions.KeyNotFoundException;
using NotImplementedException = SMTstock.Entities.Exceptions.NotImplementedException;
using UnauthorizedAccessException = SMTstock.Entities.Exceptions.UnauthorizedAccessException;
using Microsoft.Extensions.Logging;
using System.Text;
using System;

namespace SMTstock.Services.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
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
                _logger.LogError(ex,  GetExceptionMessage(ex));
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;
            message = GetExceptionMessage(exception);

            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }

            else if (exceptionType == typeof(NotFoundException))
            {
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                //message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }

        private static string GetExceptionMessage(Exception exception)
        {
            var message = new StringBuilder(exception.Message);

            if (exception.InnerException != null)
            {
                message.Append(" Inner Exception: ");
                message.Append(GetExceptionMessage(exception.InnerException));
            }

            return message.ToString();
        }
    }
}
