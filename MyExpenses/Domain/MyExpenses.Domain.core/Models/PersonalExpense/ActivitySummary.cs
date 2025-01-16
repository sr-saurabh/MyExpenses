namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class ActivitySummary
    {
        public decimal TotalSpent{ get; set; }
        public decimal TotalEarning{ get; set; }
        public decimal TotalSaving => TotalEarning - TotalSpent;
    }
}
