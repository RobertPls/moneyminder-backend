using SharedKernel.Core;

namespace Domain.ValueObjects
{
    public record EmailValue : ValueObject
    {
        public string EmailAddress { get; init; }

        public EmailValue(string emailAddress)
        {
            if (!IsValidEmailAddress(emailAddress))
            {
                throw new BussinessRuleValidationException("Invalid email address.");
            }

            EmailAddress = emailAddress;
        }

        private bool IsValidEmailAddress(string emailAddress)
        {

            return !string.IsNullOrWhiteSpace(emailAddress) && emailAddress.Contains("@");
        }

        public static implicit operator string(EmailValue value)
        {
            return value.EmailAddress;
        }

        public static implicit operator EmailValue(string emailAddress)
        {
            return new EmailValue(emailAddress);
        }
    }
}
