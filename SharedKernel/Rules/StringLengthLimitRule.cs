using SharedKernel.Core;

namespace SharedKernel.Rules
{
    public class StringLengthLimitRule : IBussinessRule
    {
        public string Value { get; }
        public int LengthLimit { get; set; }

        public StringLengthLimitRule(string value, int lengthLimit)
        {
            LengthLimit = lengthLimit;
            Value = value;
        }

        public string Message => $"Value cannot be longer than {LengthLimit} characters.";

        public bool IsValid()
        {
            return Value.Length <= LengthLimit;
        }
    }
}
