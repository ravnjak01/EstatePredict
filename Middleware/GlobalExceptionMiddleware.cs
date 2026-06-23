using System.Net;
using System.Text.Json;

namespace EstatePredict.Middleware;

/// <summary>
/// Intercepts every unhandled exception that bubbles past the service/controller
/// layer and converts it into a consistent JSON error response.
///
/// Registration order matters — register this first in Program.cs so it wraps
/// every subsequent middleware and the entire request pipeline.
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled exception on {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    // ------------------------------------------------------------------ //
    //  PRIVATE
    // ------------------------------------------------------------------ //

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = MapException(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = BuildResponse(context, exception, statusCode, title);

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Maps well-known exception types to HTTP status codes and human-readable titles.
    /// Add new mappings here as the project grows; no other file needs to change.
    /// </summary>
    private static (HttpStatusCode statusCode, string title) MapException(Exception exception)
    {
        return exception switch
        {
            // 400 – caller sent invalid data
            ArgumentException
            or ArgumentNullException
            or FormatException => (HttpStatusCode.BadRequest, "Bad Request"),

            // 401 – not authenticated
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized"),

            // 403 – authenticated but not allowed
            // (throw a custom ForbiddenException in services when needed)
            // 404 – resource not found
            KeyNotFoundException => (HttpStatusCode.NotFound, "Not Found"),

            // 409 – state conflict (duplicate name, delete blocked, etc.)
            InvalidOperationException => (HttpStatusCode.Conflict, "Conflict"),

            // 408 – slow downstream (ML service, etc.)
            TimeoutException
            or TaskCanceledException => (HttpStatusCode.RequestTimeout, "Request Timeout"),

            // 503 – external service unavailable (Python ML, SQL down, etc.)
            HttpRequestException => (HttpStatusCode.ServiceUnavailable, "Service Unavailable"),

            // 500 – anything else
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };
    }

    /// <summary>
    /// Builds the error payload.
    /// In Development the stack trace is included so developers can diagnose fast.
    /// In Production it is omitted to avoid leaking internals.
    /// </summary>
    private ErrorResponse BuildResponse(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode,
        string title)
    {
        return new ErrorResponse
        {
            Status = (int)statusCode,
            Title = title,
            Message = exception.Message,
            Path = context.Request.Path,
            TraceId = context.TraceIdentifier,
            StackTrace = _env.IsDevelopment() ? exception.StackTrace : null
        };
    }
}

// -------------------------------------------------------------------------- //
//  RESPONSE SHAPE
// -------------------------------------------------------------------------- //

/// <summary>
/// Standardised error envelope returned by the API on every failure.
/// All consumer-facing error responses share this shape.
/// </summary>
internal sealed class ErrorResponse
{
    /// <summary>HTTP status code, mirrored in the body for clients that read JSON only.</summary>
    public int Status { get; init; }

    /// <summary>Short machine-readable category (e.g. "Not Found", "Conflict").</summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>Developer-friendly detail from the exception message.</summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>Request path that triggered the error.</summary>
    public string Path { get; init; } = string.Empty;

    /// <summary>ASP.NET Core trace identifier — useful when correlating logs.</summary>
    public string TraceId { get; init; } = string.Empty;

    /// <summary>Stack trace included only in the Development environment.</summary>
    public string? StackTrace { get; init; }
}

// -------------------------------------------------------------------------- //
//  EXTENSION METHOD
// -------------------------------------------------------------------------- //

public static class GlobalExceptionMiddlewareExtensions
{
    /// <summary>
    /// Registers <see cref="GlobalExceptionMiddleware"/> as the outermost layer
    /// of the pipeline so it catches exceptions from every subsequent middleware.
    ///
    /// Usage in Program.cs:
    ///   app.UseGlobalExceptionHandler();
    /// </summary>
    public static IApplicationBuilder UseGlobalExceptionHandler(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}