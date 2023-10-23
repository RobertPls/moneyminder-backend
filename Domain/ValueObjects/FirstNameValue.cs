using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record FirstNameValue : ValueObject
    {
        public string FirstName { get; init; }

        public FirstNameValue(string firstName)
        {
            CheckRule(new StringNotNullOrEmptyRule(firstName));
            if (firstName.Length > 50)
            {
                throw new BussinessRuleValidationException("First name cannot be more than 50 characters");
            }
            FirstName = firstName;
        }

        public static implicit operator string(FirstNameValue value)
        {
            return value.FirstName;
        }

        public static implicit operator FirstNameValue(string firstName)
        {
            return new FirstNameValue(firstName);
        }
    }
}
