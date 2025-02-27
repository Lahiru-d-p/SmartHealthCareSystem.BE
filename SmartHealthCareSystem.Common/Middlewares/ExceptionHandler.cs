using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace SmartHealthCareSystem.Common.Middlewares
{
	public class ExceptionHandler
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandler> _logger;

		public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
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
				_logger.LogError(ex, "An unhandled exception occurred.");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var response = context.Response;
			response.ContentType = "application/json";

			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var includeDetails = environment == Environments.Development;

			var statusCode = exception switch
			{
				ArgumentNullException => (int)HttpStatusCode.BadRequest, // 400 - Null Argument
				ArgumentException => (int)HttpStatusCode.BadRequest,  // 400 - Bad Request (Invalid Input)
				FormatException => (int)HttpStatusCode.BadRequest, // 400 - Invalid Format
				IndexOutOfRangeException => (int)HttpStatusCode.BadRequest, // 400 - Index Out of Bounds
				DivideByZeroException => (int)HttpStatusCode.BadRequest, // 400 - Divide by Zero
				UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401 - Unauthorized
				AccessViolationException => (int)HttpStatusCode.Forbidden, // 403 - Forbidden
				KeyNotFoundException => (int)HttpStatusCode.NotFound, // 404 - Resource Not Found
				TimeoutException => (int)HttpStatusCode.RequestTimeout, // 408 - Request Timeout
				OperationCanceledException => (int)HttpStatusCode.RequestTimeout, // 408 - Operation Canceled
				InvalidOperationException => (int)HttpStatusCode.Conflict, // 409 - Conflict
				StackOverflowException => (int)HttpStatusCode.InternalServerError, // 500 - Stack Overflow
				OutOfMemoryException => (int)HttpStatusCode.InternalServerError, // 500 - Out of Memory
				SqlException => (int)HttpStatusCode.InternalServerError, // 500 - Database Error
				DbUpdateException => (int)HttpStatusCode.InternalServerError, // 500 - Database Update Failure
				NotImplementedException => (int)HttpStatusCode.NotImplemented, // 501 - Not Implemented
				HttpRequestException => (int)HttpStatusCode.ServiceUnavailable, // 503 - HTTP Request Failure
				_ => (int)HttpStatusCode.InternalServerError, // 500 - Unexpected Error
			};

			response.StatusCode = statusCode;

			var errorResponse = new
			{
				StatusCode = statusCode,
				Message = exception.Message,
				Details = includeDetails ? exception.InnerException?.Message : null
			};

			return response.WriteAsync(JsonSerializer.Serialize(errorResponse));
		}
	}
}
