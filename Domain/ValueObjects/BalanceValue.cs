using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record BalanceValue : ValueObject
    {
        public decimal Value { get; }

        public BalanceValue(decimal balance)
        {
            CheckRule(new NotNullRule(balance));
            if (balance < 0)
            {
                throw new BussinessRuleValidationException("No puede ser menor a 1");
            }
            Value = balance;
        }

        public static implicit operator decimal(BalanceValue balance)
        {
            return balance.Value;
        }

        public static implicit operator BalanceValue(decimal value)
        {
            return new BalanceValue(value);
        }
    }
}
