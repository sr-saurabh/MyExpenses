using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Entities.Base;

namespace MyExpenses.Domain.core.Entities.Common
{
    public class Goal : AuditableEntity
    {
        public int AppUserId { get; set; }

        public int CategoryId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal? Budget { get; set; }

        public decimal? TotalSpent { get; set; }

        public AppUser AppUser { get; set; }
        public Category Category { get; set; }
    }
}
