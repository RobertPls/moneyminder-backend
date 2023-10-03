using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record GuidValue : ValueObject
    {
        public Guid Guid { get; init; }

        public GuidValue (Guid guid)
        {
            CheckRule(new StringNotNullOrEmptyRule(guid.ToString()));

            if (!Guid.TryParse(guid.ToString(), out Guid parsedGuid))
            {
                throw new BussinessRuleValidationException("El valor proporcionado no es un GUID válido.");
            }
            Guid = parsedGuid;
        }

        public static implicit operator Guid(GuidValue value)
        { 
            return value.Guid;
        }

        public static implicit operator GuidValue(Guid guid)
        {
            return new GuidValue(guid);
        }
    }
}
