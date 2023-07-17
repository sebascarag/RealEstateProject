using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.Properties.Command;
using RealEstate.Application.Features.Properties.Queries;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddImage([FromForm] CreatePropertyImageCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePrice(UpdatePropertyPriceCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdatePropertyCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] GetPropertiesWithFiltersQueryRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }
    }
}
