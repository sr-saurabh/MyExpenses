namespace MyExpenses.Domain.core.Models.UserExpense
{
    public class ApiUserExpense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }

    public class ApiUserExpenseWithSummary
    {
        public List<ApiUserExpense> ApiUserExpenses { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
