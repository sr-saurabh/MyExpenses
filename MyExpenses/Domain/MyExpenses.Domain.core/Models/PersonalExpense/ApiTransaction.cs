using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class ApiTransaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
