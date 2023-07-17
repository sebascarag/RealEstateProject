using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.User.Commands;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateUserTokenCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(CreateUserCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserAdminRole(AddUserAdminRoleCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommandRequest request)
        {
            return this.OkResponse(await _mediator.Send(request));
        }
    }
}
