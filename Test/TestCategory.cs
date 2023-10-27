using Domain.Models.Categories;
using SharedKernel.Core;

namespace Test
{

    public class TestCategory
    {
        [Fact]
        public void CategoryCreation_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string categoryName = "TestCategory";
            bool isDefault = true;

            // Act
            Category category = new Category(userProfileId, categoryName, isDefault);

            // Assert
            Assert.NotNull(category.Name);
            Assert.Equal(categoryName, category.Name);
        }

        [Fact]
        public void CategoryUpdate_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string categoryName = "TestCategory";
            bool isDefault = true;
            Category category = new Category(userProfileId, categoryName, isDefault);

            // Act
            string newCategoryName = "NewCategoryName";
            category.UpdateCategory(newCategoryName);

            // Assert
            Assert.NotNull(category.Name);
            Assert.Equal(newCategoryName, category.Name);
        }

        [Fact]
        public void CategoryCreation_Should_Incorrect()
        {
            // Setup
            Guid userProfileId = Guid.Empty;
            string categoryName = "TestCategory";
            bool isDefault = true;

            // Act
            Action act = () =>
            {
                Category category = new Category(userProfileId, categoryName, isDefault);
            };

            // Assert
            var exception = Assert.Throws<BussinessRuleValidationException>(act);
            var expectedExceptionMessage = "The user cannot be empty";
            Assert.NotNull(exception);
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
