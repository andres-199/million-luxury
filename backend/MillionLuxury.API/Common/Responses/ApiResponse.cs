namespace MillionLuxury.API.Common.Responses;

public class ApiResponse
{
	public string Message { get; set; } = string.Empty;
	public int StatusCode { get; set; }
	public object? Errors { get; set; }

	public static ApiResponse Success(string message = "Operation successful", int statusCode = 200)
	{
		return new ApiResponse
		{
			Message = message,
			StatusCode = statusCode
		};
	}

	public static ApiResponse Failure(string message, int statusCode, object? errors = null)
	{
		return new ApiResponse
		{
			Message = message,
			StatusCode = statusCode,
			Errors = errors
		};
	}
}