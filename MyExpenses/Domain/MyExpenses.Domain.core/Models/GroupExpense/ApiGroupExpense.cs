namespace MyExpenses.Domain.core.Models.GroupExpense
{
    public class ApiGroupExpense
    {
        public int GroupExpenseId { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int PaidById { get; set; }
        public string PaidByName { get; set; }
        public string PaidByAvatar { get; set; }
        public List<ApiGroupExpenseShare> GroupExpenseShares { get; set; }
    }

}
