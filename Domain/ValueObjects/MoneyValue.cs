using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record MoneyValue : ValueObject
    {
        public decimal Value { get; }

        public MoneyValue(decimal balance)
        {
            CheckRule(new NotNullRule(balance));
            if (balance < 0)
            {
                throw new BussinessRuleValidationException("No puede ser menor a 1");
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
