using SharedKernel.Core;

namespace SharedKernel.Rules
{
    public class StringNotNullOrEmptyRule : IBussinessRule
    {
        private readonly string _value;

        public StringNotNullOrEmptyRule(string value)
        {
            _value = value;
        }

        public string Message => "Texto no puede ser nulo";

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(_value);
        }
    }
}
