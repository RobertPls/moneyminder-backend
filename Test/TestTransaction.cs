using Domain.Models.Transactions;

namespace Test
{
    public class TestTransaction
    {
        [Fact]
        public void TransactionCreation_Should_Correct()
        {
            // Setup
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime dateTime = DateTime.Now;
            DateTime date = dateTime.Date;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;

            // Act
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);

            // Assert
            Assert.NotNull(transaction);
            Assert.Equal(accountId, transaction.AccountId);
            Assert.Equal(categoryId, transaction.CategoryId);
            Assert.Equal(date, transaction.Date.Date);
            Assert.Equal(amount, transaction.Amount.Value);
            Assert.Equal(description, transaction.Description.Description);
            Assert.Equal(type, transaction.Type);
        }

        [Fact]
        public void TransactionUpdateDate_Should_Correct()
        {
            // Setup
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime dateTime = DateTime.Now;
            DateTime date = dateTime.Date;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);

            // Act
            DateTime newDate = DateTime.Now.AddHours(1).Date;
            transaction.UpdateDate(newDate);

            // Assert
            Assert.Equal(newDate, transaction.Date.Date);
        }

        [Fact]
        public void TransactionAddTransacionRelation_Should_Correct()
        {
            // Setup
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);
            Guid relatedTransactionId = Guid.NewGuid();

            // Act
            transaction.AddTransacionRelation(relatedTransactionId);

            // Assert
            Assert.Equal(relatedTransactionId, transaction.RelatedTransactionId);
        }

        [Fact]
        public void TransactionUpdateCategory_Should_Correct()
        {
            // Setup
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);
            Guid newCategoryId = Guid.NewGuid();

            // Act
            transaction.UpdateCategory(newCategoryId);

            // Assert
            Assert.Equal(newCategoryId, transaction.CategoryId);
        }

        [Fact]
        public void TransactionUpdated_Should_Correct()
        {
            // Setup
            Guid accountId = Guid.NewGuid();
            Guid? categoryId = null;
            DateTime date = DateTime.Now;
            decimal amount = 100;
            string description = "Test Transaction";
            TransactionType type = TransactionType.Income;
            Transaction transaction = new Transaction(accountId, categoryId, date, amount, description, type);
            decimal newAmount = 150.0M;
            TransactionType newType = TransactionType.Outcome;
            bool isTransfer = true;

            // Act
            Guid oldAccountId = transaction.AccountId;
            transaction.Updated(oldAccountId, accountId, amount, newAmount, type, newType, isTransfer);

            // Assert
            // You can add specific assertions here based on your business logic
            // For example, you may want to check that a domain event is added, etc.
        }
    }
}
