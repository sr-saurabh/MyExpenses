using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class PersonalExpenseSummary
    {
        public decimal TotalSpent{ get; set; }
        public decimal TotalEarning{ get; set; }
        public decimal TotalSaving => TotalEarning - TotalSpent;
    }
}
