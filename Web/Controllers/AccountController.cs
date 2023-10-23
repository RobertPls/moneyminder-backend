using Application.UseCase.Command.Accounts.CreateAccount;
using Application.UseCase.Command.Accounts.DeleteAccount;
using Application.UseCase.Command.Accounts.UpdateAccount;
using Application.UseCase.Query.Accounts.GetAccountListByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            command.UserId = userId;
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(new
                {
                    result
                });
            }
            else
            {
                _logger.LogError("Error creating the account");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            command.UserId = userId;
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(new
                {
                    result
                });
            }
            else
            {
                _logger.LogError("Error updating the account");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount([FromBody] DeleteAccountCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            command.UserId = userId;
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(new
                {
                    result
                });
            }
            else
            {
                _logger.LogError("Error deleting the account");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> SearchAccount()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            var query = new GetAccountListByUserIdQuery(userId);
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return Ok(new
                {
                    success = result.Success,
                    data = result.Value!.ToList(),
                    message = result.Message
                });
            }
            else
            {
                _logger.LogError("Error while searching for the user's accounts.");
                return BadRequest();
            }
        }
    }
}
