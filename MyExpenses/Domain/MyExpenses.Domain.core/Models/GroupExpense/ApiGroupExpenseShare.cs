using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.GroupExpense
{
    public class ApiGroupExpenseShare: CreateExpenseShare
    {
        public int Id { get; set; }
        public string DebitorName { get; set; }
        public string DebitorAvatar { get; set; }

    }
}
