using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Schedule.Infrastructure.Extensions;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<GlobalExceptionMiddleware> _logger;

	public GlobalExceptionMiddleware(
		RequestDelegate next,
		ILogger<GlobalExceptionMiddleware> logger)
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
			_logger.LogError(ex, "Unhandled exception occurred. Request: {Method} {Path}",
				context.Request.Method, context.Request.Path);

			await HandleExceptionAsync(context, ex);
		}
	}

	private static async Task HandleExceptionAsync(
		HttpContext context,
		Exception exception)
	{
		context.Response.ContentType = "application/json";

		(int statusCode, string message) = exception switch
		{
			InvalidOperationException => (400, exception.Message),
			ArgumentException => (400, exception.Message),
			_ => (500, "Internal server error")
		};

		context.Response.StatusCode = statusCode;

		object response = new { error = message, statusCode };
		await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
	}
}