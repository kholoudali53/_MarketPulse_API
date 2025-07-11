namespace Store.G01.APIs.Errors
{
    public class APIExceptionResponse : APIErrorResponse
    {
        public string? Details { get; set; }
        public APIExceptionResponse(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
