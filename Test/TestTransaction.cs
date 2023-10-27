using Domain.Models.Transactions;

namespace Test
{
    public class TestTransaction
    {
        [Fact]
        public void TransactionCreation_Should_Correct()
        {
            // Arrange
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;

            // Act
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(accountId, transaction.AccountId);
            Assert.Equal(categoryId, transaction.CategoryId);
            Assert.Equal(date.Date, transaction.Date.Date);
            Assert.Equal(amount, transaction.Amount.Value);
            Assert.Equal(description, transaction.Description.Description);
            Assert.Equal(type, transaction.Type);
        }

        [Fact]
        public void TransactionUpdate_Should_Correct()
        {
            // Arrange
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);

            // Act
            Guid newAccountId = Guid.NewGuid();
            Guid newCategoryId = Guid.NewGuid();
            Guid relatedTransactionId = Guid.NewGuid();
            DateTime newDate = DateTime.Now.AddHours(1).Date;
            decimal newAmount = 150.0M;
            TransactionType newType = TransactionType.Outcome;
            bool isTransference = true;

            transaction.Update(description, newAmount, newDate, newAccountId, newCategoryId, relatedTransactionId, newType, isTransference);

            // Assert
            Assert.Equal(newAccountId, transaction.AccountId);
            Assert.Equal(newCategoryId, transaction.CategoryId);
            Assert.Equal(newDate.Date, transaction.Date.Date);
            Assert.Equal(newAmount, transaction.Amount.Value);
            Assert.Equal(description, transaction.Description.Description);
            Assert.Equal(newType, transaction.Type);
        }

        [Fact]
        public void TransactionDeleteTransaction_Should_Correct()
        {
            // Arrange
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);

            // Act
            bool isTransference = false;
            transaction.DeleteTransaction(isTransference);

            // Assert
            Assert.Equal(!isTransference ? TransactionType.Outcome : TransactionType.Income, transaction.Type);
            Assert.Null(transaction.RelatedTransactionId);
        }
    }
}
