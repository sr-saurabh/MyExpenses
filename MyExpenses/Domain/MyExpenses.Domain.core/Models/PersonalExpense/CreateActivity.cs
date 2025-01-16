using MyExpenses.Domain.core.Entities.Enums;

namespace MyExpenses.Domain.core.Models.Expense
{
    public class CreateActivity
    {
        //BaseExpense Entity
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        //PersonalExpense Entity
        public int AppUserId { get; set; }
        public TransactionType Type { get; set; }
            
    }
}
