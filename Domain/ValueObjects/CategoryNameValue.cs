using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record CategoryNameValue : ValueObject
    {
        public string Name { get; init; }

        public CategoryNameValue(string name)
        {
            CheckRule(new StringNotNullOrEmptyRule(name));
            if (name.Length > 50)
            {
                throw new BussinessRuleValidationException("Name no puede tener mas de 50 caracteres");
            }
            Name = name;
        }

        public static implicit operator string(CategoryNameValue value)
        {
            return value.Name;
        }

        public static implicit operator CategoryNameValue(string name)
        {
            return new CategoryNameValue(name);
        }
    }
}

