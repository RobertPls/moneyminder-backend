using SharedKernel.Core;
using SharedKernel.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record UserNameValue : ValueObject
    {
        public string UserName { get; init; }

        public UserNameValue(string userName)
        {
            CheckRule(new StringNotNullOrEmptyRule(userName));
            if (userName.Length > 255)
            {
                throw new BussinessRuleValidationException("User name cannot be more than 255 characters");
            }
            UserName = userName;
        }

        public static implicit operator string(UserNameValue value)
        {
            return value.UserName;
        }

        public static implicit operator UserNameValue(string userName)
        {
            return new UserNameValue(userName);
        }
    }
}
