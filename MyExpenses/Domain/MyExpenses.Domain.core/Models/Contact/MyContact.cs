namespace MyExpenses.Domain.core.Models.Contact
{
    public class MyContact
    {
        public int ContactId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public decimal TotalBalance { get; set; }

    }
}
