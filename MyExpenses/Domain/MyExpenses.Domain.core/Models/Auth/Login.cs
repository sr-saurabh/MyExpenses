using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain.core.Models.Auth
{
    /// <summary>
    /// Holds the value for logging a already registered user
    /// </summary>
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
