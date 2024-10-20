using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Resenhando2.Api.Extensions;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorResponse errorResponse;
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errorResponse = ErrorResponse.ValidationError(validationException.Message);
                break;
            
            case UnauthorizedAccessException _:
                statusCode = HttpStatusCode.Unauthorized;
                errorResponse = new ErrorResponse
                {
                    Message = "Unauthorized access.",
                };
                break;
            
            case KeyNotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse
                {
                    Message = notFoundException.Message
                };
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = new ErrorResponse
                {
                    Message = "Internal server error.",
                    Details = exception.Message
                };
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

public class ErrorResponse
{
    public string? Message { get; set; }
    public string? Details { get; set; }

    public static ErrorResponse ValidationError(string validationError)
    {
        return new ErrorResponse
        {
            Message = "Validation error occurred.",
            Details = validationError
        };
    }
}