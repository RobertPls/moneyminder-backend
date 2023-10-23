using Application.UseCase.Command.Categories.CreateCategory;
using Application.UseCase.Command.Categories.DeleteCategory;
using Application.UseCase.Command.Categories.UpdateCategory;
using Application.UseCase.Query.Categories.GetCategoryListByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
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
                _logger.LogError("Error creating the category");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
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
                _logger.LogError("Error updating the category");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
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
                _logger.LogError("Error deleting the category");
                return BadRequest(new { result });
            }
        }

        [Authorize]
        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> SearchCategory()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            var query = new GetCategoryListByUserIdQuery(userId);
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
                _logger.LogError("Error while searching for the user's categories.");
                return BadRequest();
            }
        }
    }
}
