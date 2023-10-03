using SharedKernel.Core;
using System.Text.RegularExpressions;

namespace SharedKernel.Rules
{
    public class RegexRule : IBussinessRule
    {
        public string Value { get; }
        public string ValueName { get; }
        public string RegexExpression { get; }

        public RegexRule(string value, string valueName, string regexExpression)
        {
            Value = value;
            ValueName = valueName;
            RegexExpression = regexExpression;
        }

        public string Message => $"{ValueName} '{Value}' does not match the regex '{RegexExpression}'";

        public bool IsValid()
        {
            var match = Regex.Match(Value, RegexExpression, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
