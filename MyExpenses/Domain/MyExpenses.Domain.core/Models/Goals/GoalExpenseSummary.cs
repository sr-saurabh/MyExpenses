namespace MyExpenses.Domain.core.Models.Goals
{
    public class GoalExpenseSummary
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public decimal CurrentMonthExpense { get; set; }
        public decimal PreviousMonthExpense { get; set; }
    }
}