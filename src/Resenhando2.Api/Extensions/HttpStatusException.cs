namespace Resenhando2.Api.Extensions
{
    public class HttpStatusException : Exception
    {
        public int StatusCode { get; }

        protected HttpStatusException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class NotFoundException(string message) 
        : HttpStatusException(StatusCodes.Status404NotFound, message);

    public class BadRequestException(string message) 
        : HttpStatusException(StatusCodes.Status400BadRequest, message);

    public class InternalServerErrorException(string message)
        : HttpStatusException(StatusCodes.Status500InternalServerError, message);
}