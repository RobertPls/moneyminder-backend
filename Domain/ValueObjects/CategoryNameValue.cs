using SharedKernel.Core;
using SharedKernel.Rules;

namespace Domain.ValueObjects
{
    public record CategoryNameValue : ValueObject
    {
        public string CategoryName { get; init; }

        public CategoryNameValue(string categoryName)
        {
            CheckRule(new StringNotNullOrEmptyRule(categoryName));
            if (categoryName.Length > 50)
            {
                throw new BussinessRuleValidationException("Category Name cannot be more than 50 characters");
            }
            CategoryName = categoryName;
        }

        public static implicit operator string(CategoryNameValue value)
        {
            return value.CategoryName;
        }

        public static implicit operator CategoryNameValue(string categoryName)
        {
            return new CategoryNameValue(categoryName);
        }
    }
}

