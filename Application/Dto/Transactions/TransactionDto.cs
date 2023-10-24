namespace Application.Dto.Transactions
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string? RelatedTransaction { get; set; }
        public string Account { get; set; }
        public string? Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get;  set; }
        public string Type { get; set; }
    }
}
