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
            Assert.NotNull(account.Name);
            Assert.Equal(accountName, account.Name);
            Assert.Equal(userProfileId, account.UserProfileId);
            Assert.Equal(description, account.Description);
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
            account.UpdateName(newAccountName);

            // Assert
            Assert.NotNull(account.Name);
            Assert.Equal(newAccountName, account.Name);
        }

        [Fact]
        public void AccountIncreaseBalance_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string accountName = "TestAccount";
            string description = "Test Account Description";
            Account account = new Account(userProfileId, accountName, description);

            // Act
            decimal increaseAmount = 100;
            account.IncreaseBalance(increaseAmount, false);

            // Assert
            Assert.Equal(increaseAmount, account.Balance);
        }

        [Fact]
        public void AccountDecreaseBalance_Should_Correct()
        {
            // Setup
            Guid userProfileId = Guid.NewGuid();
            string accountName = "TestAccount";
            string description = "Test Account Description";
            Account account = new Account(userProfileId, accountName, description);
            decimal initialBalance = 100;
            account.IncreaseBalance(initialBalance, false);

            // Act
            decimal decreaseAmount = 50;
            account.DecreaseBalance(decreaseAmount, false);

            // Assert
            Assert.Equal(initialBalance - decreaseAmount, account.Balance);
        }

        [Fact]
        public void AccountCreation_Should_Incorrect()
        {
            // Setup
            Guid userProfileId = Guid.Empty;
            string accountName = "TestAccount";
            string description = "Test Account Description";

            // Act
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
