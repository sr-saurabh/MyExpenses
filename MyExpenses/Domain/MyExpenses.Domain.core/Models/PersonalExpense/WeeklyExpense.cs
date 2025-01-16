namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class WeeklyExpense
    {
        public List<decimal> Expenses { get; set; }
        public DateOnly StartDay{ get; set; }
        public DateOnly EndDay{ get; set; }
    }
}
