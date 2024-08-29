namespace MyExpenses.Domain.core.Models.ExpenseFilter
{
    public class AmountFilter
    {
        public decimal? Amount { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
    }
}
