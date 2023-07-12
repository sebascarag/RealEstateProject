using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Properties.Command;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(CreatePropertyCommand.Request request)
        {
            return await _mediator.Send(request);
        }
    }
}
