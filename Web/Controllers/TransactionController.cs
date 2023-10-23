using Application.UseCase.Command.Transactions.CreateTransaction;
using Application.UseCase.Command.Transactions.CreateTransference;
using Application.UseCase.Command.Transactions.DeleteTransaction;
using Application.UseCase.Command.Transactions.UpdateTransaction;
using Application.UseCase.Query.Transactions.GetFilteredUserBalance;
using Application.UseCase.Query.Transactions.GetFilteredUserTransactionList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
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
                _logger.LogError("Error creating the transaction");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [Route("transference")]
        [HttpPost]
        public async Task<IActionResult> CreateTransferencia([FromBody] CreateTransferenceCommand command)
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
                _logger.LogError("Error creating the transference");
                return BadRequest(new { result });
            }
        }
       
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdateTransactionCommand command)
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
                _logger.LogError("Error updating the transaction");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteTransaction([FromBody] DeleteTransactionCommand command)
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
                _logger.LogError("Error deleting the transaction");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> SearchTransaction([FromQuery] Guid? accountId, Guid? categoryId, string? sinceDate, string? untilDate)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            var query = new GetFilteredUserTransactionListQuery(userId, accountId, categoryId, sinceDate, untilDate);
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
                _logger.LogError("Error while searching for the user's transactions.");
                return BadRequest(new { success=false });

            }
        }

        [Authorize]
        [Route("balance/search")]
        [HttpGet]
        public async Task<IActionResult> SearchBalanceTransaction([FromQuery] string? sinceDate, string? untilDate)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            var query = new GetFilteredUserBalanceQuery(userId, sinceDate, untilDate);
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return Ok(new
                {
                    success = result.Success,
                    data=result.Value,
                    message = result.Message
                });
            }
            else
            {
                _logger.LogError("Error while searching for the user's transactions balance.");
                return BadRequest(new { success = false });
            }
        }
    }
}
