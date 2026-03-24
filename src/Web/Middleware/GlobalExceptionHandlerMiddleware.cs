using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Web.Middleware;

public class GlobalExceptionHandlerMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, exception.Message),
            BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new { error = message }, cancellationToken);

        return true;
    }
}
