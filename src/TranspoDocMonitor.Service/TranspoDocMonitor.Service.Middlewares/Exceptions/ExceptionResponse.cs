
using System.Net;


namespace TranspoDocMonitor.Service.Middlewares.Exceptions
{
    public record ExceptionResponse(
        HttpStatusCode StatusCode,
        int Code,
        string UserMessage,
        string? InnerMessage = null);
}
