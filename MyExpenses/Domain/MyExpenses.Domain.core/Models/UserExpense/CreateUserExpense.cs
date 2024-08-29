using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.UserExpense
{
    public class CreateUserExpense
    {
        /// <summary>
        /// Gets or sets the description of the expense.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the date of the expense.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the expense.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who incurred the expense.
        /// </summary>
        public int FromUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who will receive the expense amount.
        /// </summary>
        public int ToUserId { get; set; }

    }
}
