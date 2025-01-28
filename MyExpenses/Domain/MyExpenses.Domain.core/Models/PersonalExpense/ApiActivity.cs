using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.PersonalExpense
{
    public class ApiActivity
    {
        //BaseExpense Entity
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public TransactionType TransactionType { get; set; }

    }
    public class ApiActivityWithSummary
    {
        public List<ApiActivity> Expenses { get; set; }
        public ActivitySummary Summary { get; set; }
    }
    
    
}

