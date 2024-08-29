using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.ExpenseFilter
{
    public class PersonalExpenseFilter
    {
        public DateFilter? DateFilter { get; set; }
        public List<string>? Categories { get; set; }
        public TransactionType? Type { get; set; }
        public AmountFilter? AmountFilter { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
