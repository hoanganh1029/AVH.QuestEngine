using AVH.QuestEngine.Application.Responses.General;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AVH.QuestEngine.WebAPI.Base
{
    public class ApiBaseController : ControllerBase
    {
        protected ActionResult HandleResponseAsActionResult<T>(Response<T>? response)
        {
            if (response is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response.Success
                ? Ok(response.Content)
                : response.StatusCode switch
                {
                    HttpStatusCode.NotFound => NotFound(response.Message),
                    HttpStatusCode.BadRequest => BadRequest(response.Message),
                    _ => StatusCode((int)response.StatusCode, response.Message)
                };
        }
    }
}
