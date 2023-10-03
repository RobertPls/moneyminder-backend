using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record AccountNameValue : ValueObject
    {
        public string Name { get; init; }

        public AccountNameValue(string name)
        {
            CheckRule(new StringNotNullOrEmptyRule(name));
            if (name.Length > 100)
            {
                throw new BussinessRuleValidationException("Name no puede tener mas de 100 caracteres");
            }
            Name = name;
        }

        public static implicit operator string(AccountNameValue value)
        {
            return value.Name;
        }

        public static implicit operator AccountNameValue(string name)
        {
            return new AccountNameValue(name);
        }
    }
}
