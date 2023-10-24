using Application.Dto.Users;
using Application.UseCase.Query.UserProfiles.GetUserProfile;
using Application.Utils;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.ReadModel.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query.UserProfiles.GetUserProfile
{
    internal class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
    {
        private readonly DbSet<UserProfileReadModel> userProfiles;
        public GetUserProfileHandler(ReadDbContext dbContext)
        {
            userProfiles = dbContext.UserProfile;
        }
        public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await userProfiles.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (userProfile == null)
            {
                return new Result<UserProfileDto>(false, "Error");
            }

            var userProfileDto = new UserProfileDto
            {
                Id = userProfile.Id,
                FullName = userProfile.FullName,
                Balance = userProfile.Balance,
            };
            return new Result<UserProfileDto>(userProfileDto, true, "User Profile");
        }

    }
}
