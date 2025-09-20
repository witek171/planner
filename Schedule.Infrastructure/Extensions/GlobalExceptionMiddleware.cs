using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Schedule.Infrastructure.Extensions;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _requestDelegate;
	private readonly ILogger<GlobalExceptionMiddleware> _logger;
	private readonly IHostEnvironment _environment;


	public GlobalExceptionMiddleware(
		RequestDelegate requestDelegate,
		ILogger<GlobalExceptionMiddleware> logger,
		IHostEnvironment environment)
	{
		_requestDelegate = requestDelegate;
		_logger = logger;
		_environment = environment;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _requestDelegate(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Unhandled exception occurred. Request: {Method} {Path}",
				context.Request.Method, context.Request.Path);

			await HandleExceptionAsync(context, ex);
		}
	}

	private async Task HandleExceptionAsync(
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

		object response = _environment.IsDevelopment()
			? new
			{
				error = exception.Message,
				exception = exception.GetType().Name,
				statusCode,
				stackTrace = exception.StackTrace?
					.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
			}
			: new
			{
				error = message,
				statusCode
			};

		await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
	}
}