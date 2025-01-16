using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnGoalCreation
    {
        public static ModelBuilder OnGoalCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<AppUser>();

            modelBuilder.Entity<Goal>()
                        .HasOne(g => g.AppUser)
                        .WithMany(p => p.Goals)
                        .HasForeignKey(g => g.AppUserId);

            modelBuilder.Entity<Goal>()
                        .HasOne(g => g.Category)
                        .WithMany()
                        .HasForeignKey(g => g.CategoryId);


            return modelBuilder;
        }
    }
}
