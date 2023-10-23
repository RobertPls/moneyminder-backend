using SharedKernel.Core;

namespace Domain.ValueObjects
{
    public record DateValue : ValueObject
    {
        public DateTime Date { get; init; }

        public DateValue(DateTime date)
        {
            var currentDate = DateTime.Now;
            var inputDate = date.Date;

            if (inputDate > currentDate)
            {
                throw new BussinessRuleValidationException("Date cannot be in the future.");
            }

            Date = inputDate;
        }

        public static implicit operator DateTime(DateValue value)
        {
            return value.Date;
        }

        public static implicit operator DateValue(DateTime date)
        {
            return new DateValue(date);
        }
    }
}
