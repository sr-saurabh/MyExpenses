using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class ApiPersonalExpense
    {
        //BaseExpense Entity
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }


        //PersonalExpense Entity
        public string Type { get; set; }

    }
    public class ApiPersonalExpenseWithSummary
    {
        public List<ApiPersonalExpense> Expenses { get; set; }
        public PersonalExpenseSummary Summary { get; set; }
    }
    
    
}

