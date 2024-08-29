using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.UserExpense
{
    public class UpdateUserExpense : CreateUserExpense
    {
        public int Id { get; set; }
    }
}
