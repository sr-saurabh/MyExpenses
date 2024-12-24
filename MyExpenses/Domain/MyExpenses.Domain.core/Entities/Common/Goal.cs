using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MyExpenses.Domain.core.Entities.User;
using MyExpenses.Domain.core.Entities.Base;

namespace MyExpenses.Domain.core.Entities.Common
{
    public class Goal : AuditableEntity
    {
        [Required]
        public int AppUserId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        public decimal? Target { get; set; } // Optional target value for the goal

        public decimal? Progress { get; set; } // Optional progress tracking value

        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; } // Navigation property
    }
}
