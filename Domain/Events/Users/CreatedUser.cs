using SharedKernel.Core;

namespace Domain.Events.Users
{
    public record CreatedUser : DomainEvent
    {
        public Guid UserId { get; set; }


        public CreatedUser(Guid userId) : base(DateTime.Now)
        {
            UserId = userId;
        }
    }
}
