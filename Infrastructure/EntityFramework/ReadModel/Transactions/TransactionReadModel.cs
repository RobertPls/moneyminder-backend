using Infrastructure.EntityFramework.ReadModel.Accounts;
using Infrastructure.EntityFramework.ReadModel.Categories;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.EntityFramework.ReadModel.Transactions
{
    internal class TransactionReadModel
    {
        [Key]
        public Guid Id { get; set; }
        public AccountReadModel Account { get; set; }
        public Guid AccountId { get; set; }
        public CategoryReadModel? Category { get; set; }
        public Guid? CategoryId { get; set; }
        public TransactionReadModel? RelatedTransaction { get; set; }
        public Guid? RelatedTransactionId { get; set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }

    }
}
