using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.Expense
{
    public class UpdatePersonalExpense:CreatePersonalExpense
    {
        public int Id { get; set; }
    }
}
