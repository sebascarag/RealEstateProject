using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Properties.Command;
using RealEstate.Application.Properties.Queries;

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
        public async Task<ActionResult<bool>> Post(CreatePropertyCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> AddImage([FromForm] CreatePropertyImageCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<bool>> ChangePrice(UpdatePropertyPriceCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(UpdatePropertyCommandRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        public async Task<IList<PropertyFilteredDto>> Get(GetPropertiesWithFiltersQueryRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
