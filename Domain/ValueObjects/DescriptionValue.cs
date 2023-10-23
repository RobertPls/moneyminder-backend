using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record DescriptionValue : ValueObject
    {
        public string Description { get; init; }

        public DescriptionValue(string description)
        {
            CheckRule(new StringNotNullOrEmptyRule(description));
            if (description.Length > 1000)
            {
                throw new BussinessRuleValidationException("Description cannot be more than 1000 characters");
            }
            Description = description;
        }

        public static implicit operator string(DescriptionValue value)
        {
            return value.Description;
        }

        public static implicit operator DescriptionValue(string description)
        {
            return new DescriptionValue(description);
        }
    }
}
