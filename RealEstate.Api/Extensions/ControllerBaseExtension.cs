
using RealEstate.Api.Models;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtension
    {
        public static ObjectResult OkResponse(this ControllerBase controllerBase, object? result)
        {
            var response = new Response<object>()
            {
                Data = result,
                Succeeded = true,
            };
            return controllerBase.Ok(response);
        }
    }
}
