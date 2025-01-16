namespace MyExpenses.Domain.core.Models.Goals
{
    public class CreateGoal
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Budget { get; set; }
    }
}
