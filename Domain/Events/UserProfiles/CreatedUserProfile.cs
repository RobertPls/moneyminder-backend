using SharedKernel.Core;

namespace Domain.Events.UserProfiles
{
    public record CreatedUserProfile : DomainEvent
    {
        public Guid UserProfileId { get; set; }


        public CreatedUserProfile(Guid userProfileId) : base(DateTime.Now)
        {
            UserProfileId = userProfileId;
        }
    }
}
