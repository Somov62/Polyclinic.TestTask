using Azure;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Polyclinic.TestTask.API.Middlewares;

/// <summary>
/// Глобальный обработчик исключений.
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>
    /// Обработка исключения, путем возврата пользователю красивого ответа 
    /// в виде кода ошибки и сообщения с причиной ошибки.
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionMessage = string.IsNullOrEmpty(exception.InnerException?.Message) ? 
            exception.Message : 
            exception.InnerException.Message;

        _logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exceptionMessage, DateTime.UtcNow);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/text";

        await httpContext.Response.WriteAsync(exceptionMessage, cancellationToken);

        return true;
    }
}
