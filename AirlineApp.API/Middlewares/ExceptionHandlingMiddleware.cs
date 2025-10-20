using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AirlineApp.API.Middlewares;

/// <summary>
/// Middleware for centralized exception handling.
/// Catches all unhandled exceptions and returns appropriate HTTP responses.
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (KeyNotFoundException keyEx)
        {
            logger.LogWarning(keyEx, "Resource not found");
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new { error = keyEx.Message });
        }
        catch (ArgumentException argEx)
        {
            logger.LogWarning(argEx, "Invalid argument provided");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = argEx.Message });
        }
        catch (InvalidOperationException invOpEx)
        {
            logger.LogWarning(invOpEx, "Invalid operation attempted");
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            await context.Response.WriteAsJsonAsync(new { error = invOpEx.Message });
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogError(dbEx, "Database update error occurred");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = "Database update error." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                error = "An unexpected error occurred."
            });
        }
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}