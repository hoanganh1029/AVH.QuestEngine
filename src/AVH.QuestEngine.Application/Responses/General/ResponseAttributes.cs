using System.Net;

namespace AVH.QuestEngine.Application.Responses.General
{
    public abstract class ResponseAttributes
    {        
        public static Response<T> Success<T>(T content) => new()
        {
            Success = true,
            Content = content
        };

        public static Response<T> BadRequest<T>(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        public static Response<T> NotFound<T>(string message = "Entity is not found") => new()
        {
            Success = false,
            StatusCode = HttpStatusCode.NotFound,
            Message = message
        };

        public static Response Success() => new()
        {
            Success = true
        };

        public static Response NotFound(string message = "Entity is not found") => new()
        {
            Success = false,
            StatusCode = HttpStatusCode.NotFound,
            Message = message
        };

        public static Response BadRequest(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

    }
}
