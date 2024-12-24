using Microsoft.EntityFrameworkCore;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Entities.User;

namespace MyExpenses.Infrastructure.Postgres.ModelCreationHooks
{
    public static class OnCategoryCreation
    {
        public static ModelBuilder OnCategoryCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.OnAuditableEntityCreating<AppUser>();
            modelBuilder.Entity<Category>()
                        .HasOne(category => category.AppUser)
                        .WithMany(user => user.Categories)
                        .HasForeignKey(category => category.AppUserId);

            return modelBuilder;
        }
    }
}
