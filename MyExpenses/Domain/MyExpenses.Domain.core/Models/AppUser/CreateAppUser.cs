using MyExpenses.Domain.core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.AppUser
{
    public class CreateAppUser
    {
        public Guid? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public Double MonthlyBudget { get; set; }
    }
}
