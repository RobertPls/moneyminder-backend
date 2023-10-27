using Domain.Models.Accounts;
using SharedKernel.Core;

namespace Test
{
    public class TestAccount
    {
        [Fact]
        public void AccountCreation_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string accountName = "TestAccount";
            string description = "Test Account Description";

            // Act
            Account account = new Account(userProfileId, accountName, description);

            // Assert
            Assert.NotNull(account);
            Assert.Equal(userProfileId, account.UserProfileId);
            Assert.Equal(accountName, account.Name.AccountName);
            Assert.Equal(description, account.Description.Description);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void AccountUpdateName_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string accountName = "TestAccount";
            string description = "Test Account Description";
            Account account = new Account(userProfileId, accountName, description);

            // Act
            string newAccountName = "NewAccountName";
            account.UpdateAccount(newAccountName, description);

            // Assert
            Assert.Equal(newAccountName, account.Name.AccountName);
        }

        [Fact]
        public void AccountUpdateDescription_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string accountName = "TestAccount";
            string description = "Test Account Description";
            Account account = new Account(userProfileId, accountName, description);

            // Act
            string newDescription = "New Account Description";
            account.UpdateAccount(accountName, newDescription);

            // Assert
            Assert.Equal(newDescription, account.Description.Description);
        }

        [Fact]
        public void AccountCreation_WithEmptyUserProfileId_Should_ThrowException()
        {
            // Setup
            Guid userProfileId = Guid.Empty;
            string accountName = "TestAccount";
            string description = "Test Account Description";

            // Act & Assert
            Action act = () =>
            {
                Account account = new Account(userProfileId, accountName, description);
            };

            // Assert
            var exception = Assert.Throws<BussinessRuleValidationException>(act);
            var expectedExceptionMessage = "The user cannot be empty";

            Assert.NotNull(exception);
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
