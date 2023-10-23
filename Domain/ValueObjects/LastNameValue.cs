using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record LastNameValue : ValueObject
    {
        public string LastName { get; init; }

        public LastNameValue(string lastName)
        {
            CheckRule(new StringNotNullOrEmptyRule(lastName));
            if (lastName.Length > 100)
            {
                throw new BussinessRuleValidationException("Last name cannot be more than 100 characters");
            }
            LastName = lastName;
        }

        public static implicit operator string(LastNameValue value)
        {
            return value.LastName;
        }

        public static implicit operator LastNameValue(string lastName)
        {
            return new LastNameValue(lastName);
        }
    }
}
