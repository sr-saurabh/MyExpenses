using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.Expense
{
    public class CreatePersonalExpense
    {
        //BaseExpense Entity
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }


        //PersonalExpense Entity
        public int AppUserId { get; set; }
        public TransactionType Type { get; set; }
            
    }
}
