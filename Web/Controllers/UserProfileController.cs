using Application.UseCase.Query.UserProfiles.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator, ILogger<UserProfileController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Could not retrieve or parse the user ID from the JWT token.");
            }

            var query = new GetUserProfileQuery(userId);
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                return Ok(new
                {
                    success = result.Success,
                    data = result.Value,
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
