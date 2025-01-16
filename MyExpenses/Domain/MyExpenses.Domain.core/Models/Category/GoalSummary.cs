using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExpenses.Domain.core.Models.Category
{
    public class GoalSummary
    {
        public decimal Budget { get; set; }
        public decimal TargetSpent { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
