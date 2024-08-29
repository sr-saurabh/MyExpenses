namespace MyExpenses.Domain.core.Models.Settlement
{
    public class CreateSettlement
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
    }
}
