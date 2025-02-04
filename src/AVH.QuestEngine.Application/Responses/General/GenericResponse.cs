using System.Net;

namespace AVH.QuestEngine.Application.Responses.General
{
    public class Response
    {
        public virtual bool Success { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;
    }

    public class Response<T> : Response
    {
        public T? Content { get; set; }
    }

    public class SuccessResponse<T> : Response<T>
    {
        public override bool Success { get; set; } = true;
    }
}
