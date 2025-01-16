using Microsoft.AspNetCore.Routing;
using MyExpenses.Domain.core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.Accounts
{
    public class UpdateAccount
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public AccountType AccountType { get; set; }
    }
}
