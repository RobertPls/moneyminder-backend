using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record MoneyValue : ValueObject
    {
        public static MoneyValue Zero { get; } = (0);
        public decimal Value { get; }

        public MoneyValue(decimal balance)
        {
            CheckRule(new NotNullRule(balance));
            if (balance < 1)
            {
                throw new BussinessRuleValidationException("Value cannot be less than 1");
            }
            Value = balance;
        }

        public static implicit operator decimal(MoneyValue balance)
        {
            return balance.Value;
        }

        public static implicit operator MoneyValue(decimal value)
        {
            return new MoneyValue(value);
        }
    }
}
