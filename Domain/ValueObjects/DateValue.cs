using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record DateValue : ValueObject
    {
        public DateOnly Date { get; init; }

        public DateValue(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha no puede ser en el futuro.", nameof(date));
            }

            Date = date;
        }

        public static implicit operator DateOnly(DateValue value)
        {
            return value.Date;
        }

        public static implicit operator DateValue(DateOnly date)
        {
            return new DateValue(date);
        }
    }
}
