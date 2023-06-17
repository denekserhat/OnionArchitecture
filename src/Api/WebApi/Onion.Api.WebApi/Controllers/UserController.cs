using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.Api.Domain.Models;
using Onion.Common.Models.RequestedModels;

namespace Onion.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var user = await mediator.Send(new GetUserDetailQuery(id));

        //    return Ok(user);
        //}

        //[HttpGet]
        //[Route("UserName/{userName}")]
        //public async Task<IActionResult> GetByUserName(string userName)
        //{
        //    var user = await mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

        //    return Ok(user);
        //}


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res);
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        //{
        //    var guid = await mediator.Send(command);
        //    return Ok(guid);
        //}

        //[HttpPost]
        //[Route("Update")]
        //[Authorize]
        //public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        //{
        //    var guid = await mediator.Send(command);
        //    return Ok(guid);
        //}

        //[HttpPost]
        //[Route("Confirm")]
        //public async Task<IActionResult> ConfirmEmail(Guid id)
        //{
        //    var guid = await mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });
        //    return Ok(guid);
        //}

        //[HttpPost]
        //[Route("ChangePassword")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        //{
        //    if (!command.UserId.HasValue)
        //        command.UserId = UserId;

        //    var guid = await mediator.Send(command);
        //    return Ok(guid);
        //}
    }
}
