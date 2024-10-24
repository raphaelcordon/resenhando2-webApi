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
                errorResponse = new ErrorResponse(validationException.Message, null);
                break;
            
            case UnauthorizedAccessException _:
                statusCode = HttpStatusCode.Unauthorized;
                errorResponse = new ErrorResponse("Unauthorized access.", null);
                break;
            
            case KeyNotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorResponse = new ErrorResponse(notFoundException.Message, null);
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorResponse = new ErrorResponse("Internal server error.", exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}

public record ErrorResponse(string? Message, string? Details);
