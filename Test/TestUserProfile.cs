using Domain.Models.UserProfiles;
using SharedKernel.Core;

namespace Test
{
    public class UserProfileTests
    {
        [Fact]
        public void UserProfileCreation_Should_Correct()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";

            // Act
            UserProfile userProfile = new UserProfile(userId, fullName);

            // Assert
            Assert.NotNull(userProfile);
            Assert.Equal(userId, userProfile.UserId);
            Assert.Equal(fullName, userProfile.FullName);
            Assert.Equal(0, userProfile.Balance);
        }

        [Fact]
        public void UserProfileCreation_WithEmptyUserId_Should_ThrowException()
        {
            // Arrange
            Guid userId = Guid.Empty;
            string fullName = "Robert Perez";

            // Act & Assert
            Assert.Throws<BussinessRuleValidationException>(() => new UserProfile(userId, fullName));
        }

        [Fact]
        public void UserProfileIncreaseBalance_Should_Correct()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";
            int initialBalance = 100;
            UserProfile userProfile = new UserProfile(userId, fullName);
            userProfile.IncreaseBalance(initialBalance);

            // Act
            int amountToIncrease = 50;
            userProfile.IncreaseBalance(amountToIncrease);

            // Assert
            Assert.Equal(initialBalance + amountToIncrease, userProfile.Balance);
        }

        [Fact]
        public void UserProfileDecreaseBalance_Should_Correct()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";
            int initialBalance = 100;
            UserProfile userProfile = new UserProfile(userId, fullName);
            userProfile.IncreaseBalance(initialBalance);

            // Act
            int amountToDecrease = 30;
            userProfile.DecreaseBalance(amountToDecrease);

            // Assert
            Assert.Equal(initialBalance - amountToDecrease, userProfile.Balance);
        }
    }
}
