namespace MyExpenses.Domain.core.Models.Goals
{
    public class ApiGoal
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Budget { get; set; }
        public decimal TotalSpent { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
