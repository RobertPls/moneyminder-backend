using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record AccountNameValue : ValueObject
    {
        public string AccountName { get; init; }

        public AccountNameValue(string accountName)
        {
            CheckRule(new StringNotNullOrEmptyRule(accountName));
            if (accountName.Length > 100)
            {
                throw new BussinessRuleValidationException("Account Name cannot be more than 100 characters");
            }
            AccountName = accountName;
        }

        public static implicit operator string(AccountNameValue value)
        {
            return value.AccountName;
        }

        public static implicit operator AccountNameValue(string accountName)
        {
            return new AccountNameValue(accountName);
        }
    }
}
