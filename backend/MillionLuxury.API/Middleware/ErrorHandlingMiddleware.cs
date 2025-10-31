using System.Text.Json;
using FluentValidation;
using MillionLuxury.API.Common.Responses;
using MillionLuxury.Application.Common.Exceptions;
using MongoDB.Driver;

namespace MillionLuxury.API.Middleware;

public class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHandlingMiddleware> _logger;
	private readonly JsonSerializerOptions _jsonOptions;

	public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
		_jsonOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		};
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
		context.Response.ContentType = "application/json";

		var apiResponse = exception switch
		{
			ValidationException validationEx => HandleValidationException(validationEx),
			ApiException apiEx => HandleApiException(apiEx),
			MongoException mongoEx => HandleMongoException(mongoEx),
			_ => HandleUnknownException(exception)
		};

		context.Response.StatusCode = apiResponse.StatusCode;
		await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, _jsonOptions));
	}

	private ApiResponse HandleValidationException(ValidationException exception)
	{
		var errors = exception.Errors
				.GroupBy(x => x.PropertyName)
				.ToDictionary(
						g => char.ToLowerInvariant(g.Key[0]) + g.Key[1..],
						g => g.Select(x => x.ErrorMessage).ToArray()
				);

		return ApiResponse.Failure(
				"Validation error",
				StatusCodes.Status400BadRequest,
				errors);
	}

	private ApiResponse HandleApiException(ApiException exception)
	{
		return ApiResponse.Failure(
				exception.Message,
				exception.StatusCode,
				exception.Errors);
	}

	private ApiResponse HandleMongoException(MongoException exception)
	{
		_logger.LogError(exception, "MongoDB Error: {Message}", exception.Message);

		return ApiResponse.Failure(
				"Database connection error",
				StatusCodes.Status503ServiceUnavailable,
				new[] { "Could not complete the database operation." });
	}

	private ApiResponse HandleUnknownException(Exception exception)
	{
		_logger.LogError(exception, "Unhandled error: {Message}", exception.Message);

		return ApiResponse.Failure(
				"An internal error has occurred",
				StatusCodes.Status500InternalServerError);
	}
}