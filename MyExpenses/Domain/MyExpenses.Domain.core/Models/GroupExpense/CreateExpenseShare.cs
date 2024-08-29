using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.GroupExpense
{
    public class CreateExpenseShare
    {
        public int ReceiverId { get; set; }
        public decimal ShareAmount { get; set; }
    }
}
