using SharedKernel.Core;
using SharedKernel.Rules;

namespace SharedKernel.ValueObjects
{
    public record QuantityValue : ValueObject
    {
        public int Value { get; }

        public QuantityValue(int value)
        {
            CheckRule(new NotNullRule(value));
            if (value < 0)
            {
                throw new BussinessRuleValidationException("No puede ser menor a 0");
            }
            Value = value;
        }

        public static implicit operator int(QuantityValue value)
        {
            return value.Value;
        }

        public static implicit operator QuantityValue(int value)
        {
            return new QuantityValue(value);
        }
    }
}
