using Domain.Events.UserProfiles;
using Domain.Models.Transactions;
using Domain.Models.UserProfiles;
using SharedKernel.Core;

namespace Test
{
    public class UserProfileTests
    {
        [Fact]
        public void UserProfileCreation_Should_Correct()
        {
            // Setup
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
        public void UserProfileCreation_WithEmptyUserId_Should_Incorrect()
        {
            // Setup
            Guid userId = Guid.Empty;
            string fullName = "Robert Perez";

            // Act
            Action act = () =>
            {
                UserProfile userProfile = new UserProfile(userId, fullName);
            };
            var exception = Assert.Throws<BussinessRuleValidationException>(act);
            var expectedExeption = "The user cannot be empty";
            
            //Assert         
            Assert.NotNull(exception);
            Assert.Equal(exception.Message, expectedExeption);
        }

        [Fact]
        public void UserProfileUpdateBalance_Income_Should_Correct()
        {
            // Setup
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";
            UserProfile userProfile = new UserProfile(userId, fullName);

            // Act
            decimal amount = 100;
            userProfile.UpdateBalance(amount, TransactionType.Income);

            // Assert
            Assert.Equal(amount, userProfile.Balance);
        }

        [Fact]
        public void UserProfileUpdateBalance_Outcome_Should_Correct()
        {
            // Setup
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";
            UserProfile userProfile = new UserProfile(userId, fullName);
            userProfile.UpdateBalance(200, TransactionType.Income);

            // Act
            decimal amount = 50;
            userProfile.UpdateBalance(amount, TransactionType.Outcome);

            // Assert
            Assert.Equal(150, userProfile.Balance);
        }

        [Fact]
        public void UserProfileCreated_Should_AddCreatedUserProfileEvent()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string fullName = "Robert Perez";
            UserProfile userProfile = new UserProfile(userId, fullName);

            // Act
            userProfile.Created();

            // Assert
            Assert.Single(userProfile.DomainEvents);
            Assert.IsType<CreatedUserProfile>(userProfile.DomainEvents.First());
        }
    }
}
