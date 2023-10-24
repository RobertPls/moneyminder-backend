using Application.Dto.Users;
using Application.Utils;
using MediatR;

namespace Application.UseCase.Query.UserProfiles.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<Result<UserProfileDto>>
    {
        public Guid UserId { get; set; }


        public GetUserProfileQuery(Guid userId)
        {
            UserId = userId;

        }
    }
}
