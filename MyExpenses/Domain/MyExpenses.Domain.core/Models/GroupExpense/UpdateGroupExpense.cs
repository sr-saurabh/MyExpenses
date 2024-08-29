using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.GroupExpense
{
    public class UpdateGroupExpense:CreateGroupExpense
    {
        public int Id { get; set; }
        //public string Description { get; set; }
        //public string Category { get; set; }
        //public DateTime Date { get; set; }
        //public decimal Amount { get; set; }
        //public int GroupId { get; set; }
        //// user ids who paid for the expense
        //public int PayerId { get; set; }

        //// list of user ids with amount who are involved in the expenses
        //public List<UpdateExpenseShare> ExpenseShare { get; set; }
    }
}
