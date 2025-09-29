using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Schedule.Domain.Exceptions;

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

		int statusCode = exception switch
		{
			EmailAlreadyExistsException => StatusCodes.Status400BadRequest,
			PhoneAlreadyExistsException => StatusCodes.Status400BadRequest,
			ArgumentException => StatusCodes.Status400BadRequest,
			InvalidOperationException => StatusCodes.Status400BadRequest,
			_ => StatusCodes.Status500InternalServerError
		};

		context.Response.StatusCode = statusCode;

		object response = _environment.IsDevelopment()
			? new { statusCode, exceptions = GetExceptionDetails(exception) }
			: new { error = exception.Message, statusCode };

		await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
	}

	private static IEnumerable<object> GetExceptionDetails(Exception ex)
	{
		List<object> details = new();

		while (ex != null)
		{
			details.Add(new
			{
				type = ex.GetType().Name,
				message = ex.Message,
				stackTrace = ex.StackTrace?
					.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
			});

			ex = ex.InnerException;
		}

		return details;
	}
}